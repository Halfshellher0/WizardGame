using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.IO;
using System;

static public class GlobalGameParameters
{
    static public int maxBoardWidth = 20;
    static public int maxBoardHeight = 20;

    static public float NWWallOffsetX = -0.325f;
    static public float NWWallOffsetY = 0.431f;
    static public float NEWallOffsetX = 0.34f;
    static public float NEWallOffsetY = 0.415f;

}

public enum Direction
{
    northWest = 1,
    northEast = 2,
    southEast = 3,
    southWest = 4
}

public class BoardScript : MonoBehaviour {



    //Contains map data
    /*
    TileType[,] boardMap = new TileType[20,20] 
    { 
        //row0
        { TileType.walkable, TileType.walkable, TileType.unwalkable_walled , TileType.unwalkable_walled, TileType.unwalkable_walled, TileType.unwalkable_walled, TileType.unwalkable_walled, TileType.unwalkable_walled, TileType.unwalkable_walled, TileType.chest, TileType.chest, TileType.unwalkable_walled, TileType.unwalkable_walled, TileType.unwalkable_walled, TileType.unwalkable_walled, TileType.unwalkable_walled, TileType.unwalkable_walled, TileType.unwalkable_walled, TileType.walkable, TileType.walkable },
        //row1    
        {TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable},
        //row2     
        {TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled },
        //row3       
        {TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled },
        //row4    
        {TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.unwalkable_unwalled,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_unwalled,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled },
        //row5        
        {TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled },
        //row6        
        {TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.unwalkable_walled,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled },
        //row7        
        {TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.walkable,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled },
        //row8        
        {TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.unwalkable_walled },
        //row9        
        {TileType.chest,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.chest },
        //row10       
        {TileType.chest,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.chest },
        //row11     
        {TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable },
        //row12  
        {TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.walkable,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable },
        //row13      
        {TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.unwalkable_walled,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled },
        //row14      
        {TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled },
        //row15        
        {TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.unwalkable_unwalled,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.unwalkable_unwalled,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled },
        //row16       
        {TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled },
        //row17      
        {TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled },
        //row18        
        {TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable },
        //row19       
        {TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.chest,TileType.chest,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.walkable },

    };
    */


    public GameObject tile;
    public GameObject nwWall;
    public GameObject neWall;
    static public GameObject[,] Tiles;

    // Use this for initialization
    void Start () {
        GameObject go;
        GameObject nwWall_go;
        GameObject neWall_go;
        float r;

        string[] mapRows = File.ReadAllLines("E:\\GitHub\\WizardGame\\MapEdit\\MapEdit\\map2.txt");
        GlobalGameParameters.maxBoardWidth = mapRows[0].Split(',').Length;
        GlobalGameParameters.maxBoardHeight = mapRows.Length;





        Tiles = new GameObject[GlobalGameParameters.maxBoardWidth, GlobalGameParameters.maxBoardHeight];



        for (int x = 0; x < GlobalGameParameters.maxBoardWidth; x++)
        {
            for (int y = 0; y < GlobalGameParameters.maxBoardHeight; y++)
            {                
                float[] tileCoords = gridPositionToScreenPosition(x, y);

                int tileLayer = 4 * (x + y);
                int NWWallLayer = tileLayer + 1;
                int NEWallLayer = tileLayer + 2;




                go = Instantiate(tile, new Vector3(tileCoords[0], tileCoords[1], 0), Quaternion.identity) as GameObject;
                go.GetComponent<SpriteRenderer>().sortingOrder = tileLayer;

                string tileString = mapRows[y].Split(',')[x];
                TileType type = (TileType)Convert.ToInt32(tileString.Split(';')[0]);
                bool NWWall;
                bool NEWall;

                if (Convert.ToInt32(tileString.Split(';')[1]) == 0)
                {
                    NWWall = false;
                }
                else
                {
                    NWWall = true;
                    nwWall_go = Instantiate(nwWall, new Vector3(tileCoords[0] + GlobalGameParameters.NWWallOffsetX, tileCoords[1] + GlobalGameParameters.NWWallOffsetY, 0), Quaternion.identity) as GameObject;
                    nwWall_go.GetComponent<SpriteRenderer>().sortingOrder = NWWallLayer;
                }

                if (Convert.ToInt32(tileString.Split(';')[2]) == 0)
                {
                    NEWall = false;
                }
                else
                {
                    NEWall = true;
                    neWall_go = Instantiate(neWall, new Vector3(tileCoords[0] + GlobalGameParameters.NEWallOffsetX, tileCoords[1] + GlobalGameParameters.NEWallOffsetY, 0), Quaternion.identity) as GameObject;
                    neWall_go.GetComponent<SpriteRenderer>().sortingOrder = NEWallLayer;
                }


                //Set the Tiles gameobject as the tile's parent
                go.transform.parent = transform.GetChild(0);
                TileProperties tileProp = new TileProperties(x, y, type,NEWall, NWWall);
                go.SendMessage("setTileProperties", tileProp);

                Tiles[x, y] = go;                
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    float[] gridPositionToScreenPosition(int x, int y)
    {
        float[] returnFloat = new float[2] { 0f, 0f };

        returnFloat[0] = ((float)y * -0.64f) + ((float)x * 0.64f);
        returnFloat[1] = ((float)(x + y) * -0.32f);

        return returnFloat;
    }
}
