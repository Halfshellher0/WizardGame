using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapEdit
{

    public enum TileType
    {
        unwalkable_Walled = 0,
        unwalkable_Unwalled = 1,
        walkable = 2,
        spawnPoint = 3,
        chest = 4,
        essenceGenerator = 5,
    }

    public partial class Form1 : Form
    {
        //board parameters
        static public int boxSize = 30; //length of tiles in pixels
        static public int panelBoarderWidth = 10; //boarder length in pixels along the edge of the map
        static public int maxBoardY = 20;
        static public int maxBoardX = 20;
        int wallThickness = 5;

        //tile Colors
        Color unwalkable_Walled = Color.DarkGray;
        Color unwalkable_Unwalled = Color.Blue;
        Color walkable = Color.LightGray;
        Color spawnPoint = Color.Red;
        Color chest = Color.Gold;
        Color essenceGenerator = Color.Orange;

        //program Flags   
        bool editingTiles = true;
        bool editingWalls = false;

        List<TileInfo> Tiles = new List<TileInfo>();
        bool[,] wallGrid = new bool[maxBoardX * 2 + 1, maxBoardY * 2 + 1];




        public Form1()
        {
            InitializeComponent();

            createNewMap(maxBoardX, maxBoardY);
        }

        private void mapPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(mapPanel.AutoScrollPosition.X, mapPanel.AutoScrollPosition.Y);

            Pen tileOutlinePen = Pens.Black;
            Pen wallPen = new Pen(Color.Black, wallThickness);

            //Draw all tiles
            foreach (TileInfo tile in Tiles)
            {
                SolidBrush fillBrush = new SolidBrush(Color.Black);
                switch (tile.TileProp)
                {
                    case TileType.unwalkable_Walled:
                        fillBrush = new SolidBrush(unwalkable_Walled);
                        break;

                    case TileType.unwalkable_Unwalled:
                        fillBrush = new SolidBrush(unwalkable_Unwalled);
                        break;

                    case TileType.walkable:
                        fillBrush = new SolidBrush(walkable);
                        break;

                    case TileType.spawnPoint:
                        fillBrush = new SolidBrush(spawnPoint);
                        break;

                    case TileType.chest:
                        fillBrush = new SolidBrush(chest);
                        break;

                    case TileType.essenceGenerator:
                        fillBrush = new SolidBrush(essenceGenerator);
                        break;

                }

                e.Graphics.FillRectangle(fillBrush, tile.screenPos);
                e.Graphics.DrawRectangle(tileOutlinePen, tile.screenPos);
            }

            //Draw wall Lines
            Point wallPoint1 = new Point(0, 0);
            Point wallPoint2 = new Point(0, 0);
            int topLeftX = 0;
            int topLeftY = 0;
            Pen wallDrawPen = Pens.Black;

            for (int y = 0; y < wallGrid.GetLength(1); y++)
            {
                for (int x = 0; x < wallGrid.GetLength(0); x++)
                {
                    if (isValidWallSelection(x, y))
                    {
                        if (wallGrid[x, y])
                        {
                            //wall on this tile
                            wallDrawPen = wallPen;
                        }
                        else
                        {
                            //no Wall on this tile
                            wallDrawPen = tileOutlinePen;
                        }

                        if (x % 2 == 0)
                        {
                            //vertical wall
                            topLeftX = panelBoarderWidth + (x / 2) * boxSize;
                            topLeftY = panelBoarderWidth + ((y - 1) / 2) * boxSize;
                            wallPoint1 = new Point(topLeftX, topLeftY);
                            wallPoint2 = new Point(topLeftX, topLeftY + boxSize);
                        }
                        else
                        {
                            //horizontal wall
                            topLeftX = panelBoarderWidth + ((x - 1) / 2) * boxSize;
                            topLeftY = panelBoarderWidth + (y / 2) * boxSize;
                            wallPoint1 = new Point(topLeftX, topLeftY);
                            wallPoint2 = new Point(topLeftX + boxSize, topLeftY);
                        }

                        e.Graphics.DrawLine(wallDrawPen, wallPoint1, wallPoint2);
                    }
                }
            }







        }

        private void mapPanel_MouseClick(object sender, MouseEventArgs e)
        {
            int clickX = e.X - mapPanel.AutoScrollPosition.X;
            int clickY = e.Y - mapPanel.AutoScrollPosition.Y;

            if (editingTiles)
            {
                int tileX = Math.Min((clickX - panelBoarderWidth) / boxSize, maxBoardX - 1);
                int tileY = Math.Min((clickY - panelBoarderWidth) / boxSize, maxBoardY - 1);

                int listIndex = (tileY * maxBoardX) + tileX;

                TileType switchTileType = TileType.walkable;

                switch (cb_TileType.SelectedIndex)
                {
                    case 0:
                        switchTileType = TileType.unwalkable_Walled;
                        break;
                    case 1:
                        switchTileType = TileType.unwalkable_Unwalled;
                        break;
                    case 2:
                        switchTileType = TileType.walkable;
                        break;
                    case 3:
                        switchTileType = TileType.spawnPoint;
                        break;
                    case 4:
                        switchTileType = TileType.chest;
                        break;
                    case 5:
                        switchTileType = TileType.essenceGenerator;
                        break;
                }

                Tiles[listIndex].TileProp = switchTileType;
                addAdjacentWalls(tileX, tileY);
                mapPanel.Refresh();

                Console.WriteLine("{0},{1}", tileX, tileY);
            }

            if (editingWalls)
            {
                int halfBoxSize = boxSize / 2;
                int quarterBoxSize = boxSize / 4;

                int tileX = Math.Min((clickX - panelBoarderWidth + quarterBoxSize) / halfBoxSize, maxBoardX * 2);
                int tileY = Math.Min((clickY - panelBoarderWidth + quarterBoxSize) / halfBoxSize, maxBoardY * 2);

                if (isValidWallSelection(tileX, tileY))
                {
                    //if its a valid wall position, toggle wall bool
                    if (wallGrid[tileX, tileY])
                    {
                        wallGrid[tileX, tileY] = false;
                    }
                    else
                    {
                        wallGrid[tileX, tileY] = true;
                    }
                }
                mapPanel.Refresh();

                Console.WriteLine("{0},{1}", tileX, tileY);

            }
        }

        private void bt_EditWalls_Click(object sender, EventArgs e)
        {
            editingWalls = true;
            editingTiles = false;
            bt_EditWalls.BackColor = Color.Gold;
            bt_EditTiles.BackColor = SystemColors.Control;
        }

        private void bt_EditTiles_Click(object sender, EventArgs e)
        {
            editingWalls = false;
            editingTiles = true;
            bt_EditTiles.BackColor = Color.Gold;
            bt_EditWalls.BackColor = SystemColors.Control;
        }

        private bool isValidWallSelection(int x, int y)
        {
            bool returnBool = true;

            if (x % 2 == 0)
            {
                //x is even
                if (y % 2 == 0)
                {
                    //y is even.
                    returnBool = false;
                }
                else
                {
                    //y is odd
                }
            }
            else
            {
                //x is odd
                if (y % 2 == 0)
                {
                    //y is even.
                }
                else
                {
                    //y is odd
                    returnBool = false;
                }
            }

            return returnBool;
        }


        private void mapPanel_MouseMove(object sender, MouseEventArgs e)
        {
            int clickX = e.X - mapPanel.AutoScrollPosition.X;
            int clickY = e.Y - mapPanel.AutoScrollPosition.Y;
            int tileX = Math.Min((clickX - panelBoarderWidth) / boxSize, maxBoardX - 1);
            int tileY = Math.Min((clickY - panelBoarderWidth) / boxSize, maxBoardY - 1);

            lb_CurrentCoords.Text = "(" + tileX + "," + tileY + ")";
        }

        private void addAdjacentWalls(int x, int y)
        {
            int listIndex = (y * maxBoardX) + x;
            int prevX = x - 1;
            int nextX = x + 1;
            int prevY = y - 1;
            int nextY = y + 1;


            if (Tiles[listIndex].TileProp == TileType.unwalkable_Walled)
            {
                //tile is walled, check for adjacent unwalled tiles
                if (prevX < 0)
                {
                    int wallX = 0;
                    int wallY = (y * 2) + 1;

                    wallGrid[wallX, wallY] = false;
                }
                else
                {
                    listIndex = (y * maxBoardX) + prevX;
                    TileType adjactentTile = Tiles[listIndex].TileProp;
                    int wallX = (x * 2);
                    int wallY = (y * 2) + 1;
                    if (adjactentTile != TileType.unwalkable_Walled)
                    {
                        wallGrid[wallX, wallY] = true;
                    }
                    else
                    {
                        wallGrid[wallX, wallY] = false;
                    }
                }

                if (nextX >= maxBoardX)
                {
                    int wallX = maxBoardX * 2;
                    int wallY = (y * 2) + 1;

                    wallGrid[wallX, wallY] = false;
                }
                else
                {
                    listIndex = (y * maxBoardX) + nextX;
                    TileType adjactentTile = Tiles[listIndex].TileProp;
                    int wallX = ((x + 1) * 2);
                    int wallY = (y * 2) + 1;
                    if (adjactentTile != TileType.unwalkable_Walled)
                    {
                        wallGrid[wallX, wallY] = true;
                    }
                    else
                    {
                        wallGrid[wallX, wallY] = false;
                    }
                }

                if (prevY < 0)
                {
                    int wallY = 0;
                    int wallX = (x * 2) + 1;

                    wallGrid[wallX, wallY] = false;
                }
                else
                {
                    listIndex = (prevY * maxBoardX) + x;
                    TileType adjactentTile = Tiles[listIndex].TileProp;
                    int wallX = (x * 2) + 1;
                    int wallY = (y * 2);
                    if (adjactentTile != TileType.unwalkable_Walled)
                    {
                        wallGrid[wallX, wallY] = true;
                    }
                    else
                    {
                        wallGrid[wallX, wallY] = false;
                    }
                }

                if (nextY >= maxBoardY)
                {
                    int wallY = maxBoardY * 2;
                    int wallX = (x * 2) + 1;

                    wallGrid[wallX, wallY] = false;
                }
                else
                {
                    listIndex = (nextY * maxBoardX) + x;
                    TileType adjactentTile = Tiles[listIndex].TileProp;
                    int wallX = (x * 2) + 1;
                    int wallY = ((y + 1) * 2);
                    if (adjactentTile != TileType.unwalkable_Walled)
                    {
                        wallGrid[wallX, wallY] = true;
                    }
                    else
                    {
                        wallGrid[wallX, wallY] = false;
                    }
                }
            }
            else
            {
                //tile is unwalled, check for adjacent walled tiles
                if (prevX < 0)
                {
                    int wallX = 0;
                    int wallY = (y * 2) + 1;

                    wallGrid[wallX, wallY] = true;
                }
                else
                {
                    listIndex = (y * maxBoardX) + prevX;
                    TileType adjactentTile = Tiles[listIndex].TileProp;
                    int wallX = (x * 2);
                    int wallY = (y * 2) + 1;
                    if (adjactentTile == TileType.unwalkable_Walled)
                    {
                        wallGrid[wallX, wallY] = true;
                    }
                    else
                    {
                        wallGrid[wallX, wallY] = false;
                    }
                }

                if (nextX >= maxBoardX)
                {
                    int wallX = maxBoardX * 2;
                    int wallY = (y * 2) + 1;

                    wallGrid[wallX, wallY] = true;
                }
                else
                {
                    listIndex = (y * maxBoardX) + nextX;
                    TileType adjactentTile = Tiles[listIndex].TileProp;
                    int wallX = ((x + 1) * 2);
                    int wallY = (y * 2) + 1;
                    if (adjactentTile == TileType.unwalkable_Walled)
                    {
                        wallGrid[wallX, wallY] = true;
                    }
                    else
                    {
                        wallGrid[wallX, wallY] = false;
                    }
                }

                if (prevY < 0)
                {
                    int wallY = 0;
                    int wallX = (x * 2) + 1;

                    wallGrid[wallX, wallY] = true;
                }
                else
                {
                    listIndex = (prevY * maxBoardX) + x;
                    TileType adjactentTile = Tiles[listIndex].TileProp;
                    int wallX = (x * 2) + 1;
                    int wallY = (y * 2);
                    if (adjactentTile == TileType.unwalkable_Walled)
                    {
                        wallGrid[wallX, wallY] = true;
                    }
                    else
                    {
                        wallGrid[wallX, wallY] = false;
                    }
                }

                if (nextY >= maxBoardY)
                {
                    int wallY = maxBoardY * 2;
                    int wallX = (x * 2) + 1;

                    wallGrid[wallX, wallY] = true;
                }
                else
                {
                    listIndex = (nextY * maxBoardX) + x;
                    TileType adjactentTile = Tiles[listIndex].TileProp;
                    int wallX = (x * 2) + 1;
                    int wallY = ((y + 1) * 2);
                    if (adjactentTile == TileType.unwalkable_Walled)
                    {
                        wallGrid[wallX, wallY] = true;
                    }
                    else
                    {
                        wallGrid[wallX, wallY] = false;
                    }
                }

            }
        }

        private void createNewMap(int MaxBoardX, int MaxBoardY)
        {
            maxBoardX = MaxBoardX;
            maxBoardY = MaxBoardY;

            int maxX = (panelBoarderWidth * 2) + (maxBoardX * boxSize);
            int maxY = (panelBoarderWidth * 2) + (maxBoardY * boxSize);

            mapPanel.AutoScrollMinSize = new Size(maxX, maxY);

            Tiles = new List<TileInfo>();
            wallGrid = new bool[maxBoardX * 2 + 1, maxBoardY * 2 + 1];



            TileInfo tile;

            //Initialize Tiles
            for (int y = 0; y < maxBoardY; y++)
            {
                for (int x = 0; x < maxBoardX; x++)
                {
                    tile = new TileInfo(x, y, TileType.walkable, boxSize, panelBoarderWidth);
                    Tiles.Add(tile);
                }
            }

            //Initialize Walls
            for (int y = 0; y < wallGrid.GetLength(1); y++)
            {
                for (int x = 0; x < wallGrid.GetLength(0); x++)
                {
                    wallGrid[x, y] = false;
                }
            }

            //add walls around the outside
            for (int y = 0; y < maxBoardY; y++)
            {
                for (int x = 0; x < maxBoardX; x++)
                {
                    addAdjacentWalls(x, y);
                }
            }

            mapPanel.Refresh();
        }

        private void bt_SaveMap_Click(object sender, EventArgs e)
        {
            DialogResult save = saveFileDialog1.ShowDialog();

            if (save == DialogResult.OK)
            {

                string fileText = "";

                for (int y = 0; y < maxBoardY; y++)
                {
                    string fileLine = "";
                    for (int x = 0; x < maxBoardX; x++)
                    {
                        int listIndex = (y * maxBoardX) + x;
                        int tileType = -1;
                        int NWWall = -1;
                        int NEWall = -1;

                        tileType = Convert.ToInt32(Tiles[listIndex].TileProp);
                        NWWall = Convert.ToInt32(wallGrid[x * 2, (y * 2) + 1]);
                        NEWall = Convert.ToInt32(wallGrid[(x * 2) + 1, y * 2]);

                        fileLine = fileLine + tileType.ToString() + ";" + NWWall.ToString() + ";" + NEWall.ToString() + ",";
                    }

                    fileLine = fileLine.TrimEnd(',');
                    fileText = fileText + fileLine + Environment.NewLine;
                }

                File.WriteAllText(saveFileDialog1.FileName, fileText);
            }

        }

        private void bt_LoadMap_Click(object sender, EventArgs e)
        {

            DialogResult open = openFileDialog1.ShowDialog();

            if (open == DialogResult.OK)
            {

                string[] fileLines = File.ReadAllLines(openFileDialog1.FileName);
                int x = 0;
                int y = 0;


                maxBoardY = fileLines.Length;
                maxBoardX = fileLines[0].Split(',').Length;

                Tiles = new List<TileInfo>();
                wallGrid = new bool[maxBoardX * 2 + 1, maxBoardY * 2 + 1];

                int maxX = (panelBoarderWidth * 2) + (maxBoardX * boxSize);
                int maxY = (panelBoarderWidth * 2) + (maxBoardY * boxSize);

                mapPanel.AutoScrollMinSize = new Size(maxX, maxY);


                //Initialize Walls
                for (int i = 0; i < wallGrid.GetLength(1); i++)
                {
                    for (int j = 0; j < wallGrid.GetLength(0); j++)
                    {
                        wallGrid[j, i] = false;
                    }
                }

                foreach (string line in fileLines)
                {
                    string[] tileData = line.Split(',');
                    x = 0;

                    foreach (string tileProp in tileData)
                    {
                        int tileType = Convert.ToInt32(tileProp.Split(';')[0]);
                        bool NWWall;
                        bool NEWall;
                        bool SWWall = false;
                        bool SEWall = false;

                        if (tileProp.Split(';')[1] == "1")
                        {
                            NWWall = true;
                        }
                        else
                        {
                            NWWall = false;
                        }

                        if (tileProp.Split(';')[2] == "1")
                        {
                            NEWall = true;
                        }
                        else
                        {
                            NEWall = false;
                        }

                        if (x == maxBoardX - 1 && (TileType)tileType != TileType.unwalkable_Walled)
                        {
                            SEWall = true;
                        }

                        if (y == maxBoardY - 1 && (TileType)tileType != TileType.unwalkable_Walled)
                        {
                            SWWall = true;
                        }


                        Tiles.Add(new TileInfo(x, y, (TileType)tileType, boxSize, panelBoarderWidth));
                        wallGrid[x * 2, (y * 2) + 1] = NWWall;
                        wallGrid[(x * 2) + 1, y * 2] = NEWall;

                        if (SEWall)
                        {
                            wallGrid[(x + 1) * 2, (y * 2) + 1] = SEWall;
                        }

                        if (SWWall)
                        {
                            wallGrid[(x * 2) + 1, (y + 1) * 2] = SWWall;
                        }


                        x++;
                    }


                    y++;
                }
                mapPanel.Refresh();
            }
        }

        private void bt_CreateFile_Click(object sender, EventArgs e)
        {
            try
            {
                int boardX = Convert.ToInt32(tb_BoardX.Text);
                int boardY = Convert.ToInt32(tb_BoardY.Text);
                createNewMap(boardX, boardY);
            }
            catch
            {

            }
        }

        private void sb_MapScale_Scroll(object sender, ScrollEventArgs e)
        {
            boxSize = sb_MapScale.Value;
            foreach (TileInfo Tile in Tiles)
            {
                Tile.updateBoxSize(boxSize, panelBoarderWidth);
            }

            int maxX = (panelBoarderWidth * 2) + (maxBoardX * boxSize);
            int maxY = (panelBoarderWidth * 2) + (maxBoardY * boxSize);

            mapPanel.AutoScrollMinSize = new Size(maxX, maxY);

            mapPanel.Refresh();
        }

        private void bt_Print_Click(object sender, EventArgs e)
        {


            int boxSizeX = (1100 - (2 * panelBoarderWidth)) / maxBoardX;
            int boxSizeY = (850 - (2 * panelBoarderWidth)) / maxBoardY;
            int newBoxSize = Math.Min(boxSizeX, boxSizeY);

            int imageWidth = (2 * panelBoarderWidth) + (maxBoardX * newBoxSize);
            int imageHeight = (2 * panelBoarderWidth) + (maxBoardY * newBoxSize);

            foreach (TileInfo Tile in Tiles)
            {
                Tile.updateBoxSize(newBoxSize, panelBoarderWidth);
            }

            using (Bitmap bitmap = new Bitmap(imageWidth, imageHeight))
            {
                Graphics g = Graphics.FromImage(bitmap);

                Pen tileOutlinePen = new Pen(Color.Black, 1);
                Pen wallPen = new Pen(Color.Black, wallThickness);

                //Draw all tiles
                foreach (TileInfo tile in Tiles)
                {
                    SolidBrush fillBrush = new SolidBrush(Color.Black);
                    switch (tile.TileProp)
                    {
                        case TileType.unwalkable_Walled:
                            fillBrush = new SolidBrush(unwalkable_Walled);
                            break;

                        case TileType.unwalkable_Unwalled:
                            fillBrush = new SolidBrush(unwalkable_Unwalled);
                            break;

                        case TileType.walkable:
                            fillBrush = new SolidBrush(walkable);
                            break;

                        case TileType.spawnPoint:
                            fillBrush = new SolidBrush(spawnPoint);
                            break;

                        case TileType.chest:
                            fillBrush = new SolidBrush(chest);
                            break;

                        case TileType.essenceGenerator:
                            fillBrush = new SolidBrush(essenceGenerator);
                            break;

                    }

                    g.FillRectangle(fillBrush, tile.screenPos);
                    g.DrawRectangle(tileOutlinePen, tile.screenPos);
                }

                foreach (TileInfo Tile in Tiles)
                {
                    Tile.updateBoxSize(boxSize, panelBoarderWidth);
                }

                //Draw wall Lines
                Point wallPoint1 = new Point(0, 0);
                Point wallPoint2 = new Point(0, 0);
                int topLeftX = 0;
                int topLeftY = 0;
                Pen wallDrawPen = Pens.Black;

                for (int y = 0; y < wallGrid.GetLength(1); y++)
                {
                    for (int x = 0; x < wallGrid.GetLength(0); x++)
                    {
                        if (isValidWallSelection(x, y))
                        {
                            if (wallGrid[x, y])
                            {
                                //wall on this tile
                                wallDrawPen = wallPen;
                            }
                            else
                            {
                                //no Wall on this tile
                                wallDrawPen = tileOutlinePen;
                            }

                            if (x % 2 == 0)
                            {
                                //vertical wall
                                topLeftX = panelBoarderWidth + (x / 2) * newBoxSize;
                                topLeftY = panelBoarderWidth + ((y - 1) / 2) * newBoxSize;
                                wallPoint1 = new Point(topLeftX, topLeftY);
                                wallPoint2 = new Point(topLeftX, topLeftY + newBoxSize);
                            }
                            else
                            {
                                //horizontal wall
                                topLeftX = panelBoarderWidth + ((x - 1) / 2) * newBoxSize;
                                topLeftY = panelBoarderWidth + (y / 2) * newBoxSize;
                                wallPoint1 = new Point(topLeftX, topLeftY);
                                wallPoint2 = new Point(topLeftX + newBoxSize, topLeftY);
                            }

                            g.DrawLine(wallDrawPen, wallPoint1, wallPoint2);
                        }
                    }
                }


                Rectangle imageArea = new Rectangle(0, 0, imageWidth, imageHeight);
                //bitmap.Save("C:\\test.bmp", ImageFormat.Bmp);

                PrintDocument pd = new PrintDocument();

                PrintDialog pdi = new PrintDialog();
                pdi.Document = pd;
                pdi.UseEXDialog = true;

                pd.DefaultPageSettings.Landscape = true; //or false!
                pd.PrintPage += (sender2, args) =>
                {
                    Size bitmapSize = bitmap.Size;
                    Rectangle pagePosition = new Rectangle(0, 0, bitmapSize.Width, bitmap.Height);

                    Rectangle m = args.MarginBounds;
                    /*
                    m.X = 0;
                    m.Y = 0;
                    m.Height = m.Height + 200;
                    m.Width = m.Width + 200;


                    if ((double)bitmap.Width / (double)bitmap.Height > (double)m.Width / (double)m.Height) // image is wider
                    {
                        m.Height = (int)((double)bitmap.Height / (double)bitmap.Width * (double)m.Width);
                    }
                    else
                    {
                        m.Width = (int)((double)bitmap.Width / (double)bitmap.Height * (double)m.Height);
                    }
                    */
                    args.Graphics.DrawImage(bitmap, pagePosition);
                };

                if (pdi.ShowDialog() == DialogResult.OK)
                {
                    pd.Print();
                }
                else
                {
                    //print cancelled
                }

            }
        }
    }



    public class TileInfo
    {
        public int X;
        public int Y;
        public TileType TileProp;
        public Rectangle screenPos;


        public TileInfo(int x, int y, TileType tileProp, int boxSize, int panelBoarderWidth)
        {
            X = x;
            Y = y;
            TileProp = tileProp;


            int startX = (X * boxSize) + panelBoarderWidth;
            int startY = (Y * boxSize) + panelBoarderWidth;

            screenPos = new Rectangle(startX, startY, boxSize, boxSize);

        }

        public void updateBoxSize(int BoxSize, int PanelBoarderWidth)
        {
            int startX = (X * BoxSize) + PanelBoarderWidth;
            int startY = (Y * BoxSize) + PanelBoarderWidth;

            screenPos = new Rectangle(startX, startY, BoxSize, BoxSize);
        }
    }
}
