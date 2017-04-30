namespace MapEdit
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mapPanel = new System.Windows.Forms.Panel();
            this.cb_TileType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bt_EditTiles = new System.Windows.Forms.Button();
            this.bt_EditWalls = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_CurrentCoords = new System.Windows.Forms.Label();
            this.bt_SaveMap = new System.Windows.Forms.Button();
            this.bt_LoadMap = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.bt_CreateFile = new System.Windows.Forms.Button();
            this.tb_BoardX = new System.Windows.Forms.TextBox();
            this.tb_BoardY = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bt_Print = new System.Windows.Forms.Button();
            this.sb_MapScale = new System.Windows.Forms.HScrollBar();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapPanel
            // 
            this.mapPanel.AutoScroll = true;
            this.mapPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mapPanel.Location = new System.Drawing.Point(12, 12);
            this.mapPanel.Name = "mapPanel";
            this.mapPanel.Size = new System.Drawing.Size(1425, 976);
            this.mapPanel.TabIndex = 0;
            this.mapPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mapPanel_Paint);
            this.mapPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapPanel_MouseClick);
            this.mapPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapPanel_MouseMove);
            // 
            // cb_TileType
            // 
            this.cb_TileType.FormattingEnabled = true;
            this.cb_TileType.Items.AddRange(new object[] {
            "unwalkable_Walled",
            "unwalkable_Unwalled",
            "walkable",
            "spawnPoint",
            "chest",
            "essenceGenerator"});
            this.cb_TileType.Location = new System.Drawing.Point(6, 28);
            this.cb_TileType.Name = "cb_TileType";
            this.cb_TileType.Size = new System.Drawing.Size(182, 21);
            this.cb_TileType.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Draw Tile Types:";
            // 
            // bt_EditTiles
            // 
            this.bt_EditTiles.BackColor = System.Drawing.Color.Gold;
            this.bt_EditTiles.Location = new System.Drawing.Point(6, 86);
            this.bt_EditTiles.Name = "bt_EditTiles";
            this.bt_EditTiles.Size = new System.Drawing.Size(75, 23);
            this.bt_EditTiles.TabIndex = 3;
            this.bt_EditTiles.Text = "Edit Tiles";
            this.bt_EditTiles.UseVisualStyleBackColor = false;
            this.bt_EditTiles.Click += new System.EventHandler(this.bt_EditTiles_Click);
            // 
            // bt_EditWalls
            // 
            this.bt_EditWalls.Location = new System.Drawing.Point(6, 115);
            this.bt_EditWalls.Name = "bt_EditWalls";
            this.bt_EditWalls.Size = new System.Drawing.Size(75, 23);
            this.bt_EditWalls.TabIndex = 4;
            this.bt_EditWalls.Text = "Edit Walls";
            this.bt_EditWalls.UseVisualStyleBackColor = true;
            this.bt_EditWalls.Click += new System.EventHandler(this.bt_EditWalls_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 393);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Currently Over Tile:";
            // 
            // lb_CurrentCoords
            // 
            this.lb_CurrentCoords.AutoSize = true;
            this.lb_CurrentCoords.Location = new System.Drawing.Point(3, 406);
            this.lb_CurrentCoords.Name = "lb_CurrentCoords";
            this.lb_CurrentCoords.Size = new System.Drawing.Size(26, 13);
            this.lb_CurrentCoords.TabIndex = 6;
            this.lb_CurrentCoords.Text = "(x,y)";
            // 
            // bt_SaveMap
            // 
            this.bt_SaveMap.Location = new System.Drawing.Point(6, 224);
            this.bt_SaveMap.Name = "bt_SaveMap";
            this.bt_SaveMap.Size = new System.Drawing.Size(94, 23);
            this.bt_SaveMap.TabIndex = 7;
            this.bt_SaveMap.Text = "Save Map File";
            this.bt_SaveMap.UseVisualStyleBackColor = true;
            this.bt_SaveMap.Click += new System.EventHandler(this.bt_SaveMap_Click);
            // 
            // bt_LoadMap
            // 
            this.bt_LoadMap.Location = new System.Drawing.Point(6, 253);
            this.bt_LoadMap.Name = "bt_LoadMap";
            this.bt_LoadMap.Size = new System.Drawing.Size(94, 23);
            this.bt_LoadMap.TabIndex = 8;
            this.bt_LoadMap.Text = "Load Map File";
            this.bt_LoadMap.UseVisualStyleBackColor = true;
            this.bt_LoadMap.Click += new System.EventHandler(this.bt_LoadMap_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // bt_CreateFile
            // 
            this.bt_CreateFile.Location = new System.Drawing.Point(6, 298);
            this.bt_CreateFile.Name = "bt_CreateFile";
            this.bt_CreateFile.Size = new System.Drawing.Size(113, 23);
            this.bt_CreateFile.TabIndex = 9;
            this.bt_CreateFile.Text = "Create New Map";
            this.bt_CreateFile.UseVisualStyleBackColor = true;
            this.bt_CreateFile.Click += new System.EventHandler(this.bt_CreateFile_Click);
            // 
            // tb_BoardX
            // 
            this.tb_BoardX.Location = new System.Drawing.Point(102, 338);
            this.tb_BoardX.Name = "tb_BoardX";
            this.tb_BoardX.Size = new System.Drawing.Size(58, 20);
            this.tb_BoardX.TabIndex = 10;
            this.tb_BoardX.Text = "20";
            // 
            // tb_BoardY
            // 
            this.tb_BoardY.Location = new System.Drawing.Point(102, 364);
            this.tb_BoardY.Name = "tb_BoardY";
            this.tb_BoardY.Size = new System.Drawing.Size(58, 20);
            this.tb_BoardY.TabIndex = 11;
            this.tb_BoardY.Text = "20";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 367);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Board Length (y)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 341);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Board Width (x)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bt_Print);
            this.panel1.Controls.Add(this.sb_MapScale);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cb_TileType);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.bt_EditTiles);
            this.panel1.Controls.Add(this.tb_BoardY);
            this.panel1.Controls.Add(this.bt_EditWalls);
            this.panel1.Controls.Add(this.tb_BoardX);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.bt_CreateFile);
            this.panel1.Controls.Add(this.lb_CurrentCoords);
            this.panel1.Controls.Add(this.bt_LoadMap);
            this.panel1.Controls.Add(this.bt_SaveMap);
            this.panel1.Location = new System.Drawing.Point(1443, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(210, 976);
            this.panel1.TabIndex = 14;
            // 
            // bt_Print
            // 
            this.bt_Print.Location = new System.Drawing.Point(6, 633);
            this.bt_Print.Name = "bt_Print";
            this.bt_Print.Size = new System.Drawing.Size(155, 23);
            this.bt_Print.TabIndex = 16;
            this.bt_Print.Text = "Print for Play Test";
            this.bt_Print.UseVisualStyleBackColor = true;
            this.bt_Print.Click += new System.EventHandler(this.bt_Print_Click);
            // 
            // sb_MapScale
            // 
            this.sb_MapScale.Location = new System.Drawing.Point(14, 493);
            this.sb_MapScale.Minimum = 10;
            this.sb_MapScale.Name = "sb_MapScale";
            this.sb_MapScale.Size = new System.Drawing.Size(174, 22);
            this.sb_MapScale.TabIndex = 15;
            this.sb_MapScale.Value = 30;
            this.sb_MapScale.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sb_MapScale_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 469);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Map Scale:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1665, 1000);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapPanel);
            this.Name = "Form1";
            this.Text = "Map Editor";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mapPanel;
        private System.Windows.Forms.ComboBox cb_TileType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_EditTiles;
        private System.Windows.Forms.Button bt_EditWalls;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_CurrentCoords;
        private System.Windows.Forms.Button bt_SaveMap;
        private System.Windows.Forms.Button bt_LoadMap;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button bt_CreateFile;
        private System.Windows.Forms.TextBox tb_BoardX;
        private System.Windows.Forms.TextBox tb_BoardY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.HScrollBar sb_MapScale;
        private System.Windows.Forms.Button bt_Print;
    }
}

