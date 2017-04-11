using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

static public class GlobalGameParameters
{
    static public int maxBoardWidth = 20;
    static public int maxBoardHeight = 20;
}

public enum Direction
{
    northWest = 1,
    northEast = 2,
    southEast = 3,
    southWest = 4
}

public class BoardScript : MonoBehaviour {    

    public GameObject tile;
    public GameObject wizard;
    float wizardTileOffsetX = 0.01f;
    float wizardTileOffsetY = -0.45f;

    //Contains map data
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
        {TileType.chest,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_unwalled,TileType.unwalkable_unwalled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.chest },
        //row10       
        {TileType.chest,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_unwalled,TileType.unwalkable_unwalled,TileType.walkable,TileType.walkable,TileType.walkable,TileType.walkable,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.unwalkable_walled,TileType.chest },
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

    static public GameObject[,] Tiles = new GameObject[GlobalGameParameters.maxBoardWidth, GlobalGameParameters.maxBoardHeight];

    // Use this for initialization
    void Start () {
        GameObject go;        
        float r;
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {                
                float[] tileCoords = gridPositionToScreenPosition(x, y);
                go = Instantiate(tile, new Vector3(tileCoords[0], tileCoords[1], 0), Quaternion.identity) as GameObject;
                
                //Set the Tiles gameobject as the tile's parent
                go.transform.parent = transform.GetChild(0);
                TileProperties tileProp = new TileProperties(x, y, boardMap[y, x]);
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
        returnFloat[1] = ((float)(x + y) * 0.32f) - 6.08f;

        return returnFloat;
    }
}
