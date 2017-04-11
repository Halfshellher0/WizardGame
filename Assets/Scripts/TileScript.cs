using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileProperties
{
    public TileType TileType;
    public int X;
    public int Y;

    public TileProperties(int x, int y, TileType tileType)
    {
        TileType = tileType;
        X = x;
        Y = y;
    }

} 

public enum TileType
{
    walkable = 1,
    unwalkable_unwalled = 2,
    unwalkable_walled = 3,
    chest = 4
}

public class TileScript : MonoBehaviour {


    public int XCoord;
    public int YCoord;
    public bool isWalkable;

    Color previousColor;

	// Use this for initialization
	void Start ()
    {
     
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseEnter()
    {
        if (isWalkable)
        {
            previousColor = this.GetComponent<SpriteRenderer>().color;
            this.GetComponent<SpriteRenderer>().color = Color.magenta;

            List<Node> walkPath = Pathfinding.findPathToDestination(9, 11, XCoord, YCoord);
            foreach(Node node in walkPath)
            {
                BoardScript.Tiles[node.X, node.Y].GetComponent<SpriteRenderer>().color = Color.yellow;
            }

        }
    }

    void OnMouseExit()
    {
        if (isWalkable)
        {
            this.GetComponent<SpriteRenderer>().color = previousColor;
        }
    }

    //set the tile properties based on tiletype
    void setTileProperties(TileProperties tileProp)
    {
        switch(tileProp.TileType)
        {
            case TileType.walkable:
                isWalkable = true;
                break;
            case TileType.chest:
                isWalkable = true;
                break;
            case TileType.unwalkable_unwalled:
                this.GetComponent<SpriteRenderer>().color = Color.blue;
                isWalkable = false;
                break;
            case TileType.unwalkable_walled:
                this.GetComponent<SpriteRenderer>().color = Color.black;
                isWalkable = false;
                break;
        }

        XCoord = tileProp.X;
        YCoord = tileProp.Y;
    }
}
