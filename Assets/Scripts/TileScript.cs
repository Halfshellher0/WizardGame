using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileProperties
{
    public TileType TileType;
    public int X;
    public int Y;
    public bool NEWall;
    public bool NWWall;

    


    public TileProperties(int x, int y, TileType tileType,bool neWall, bool nwWall)
    {
        TileType = tileType;
        X = x;
        Y = y;
        NEWall = neWall;
        NWWall = nwWall;
    }

} 

public enum TileType
{
    unwalkable_walled = 0,
    unwalkable_unwalled = 1,
    walkable = 2,
    spawnPoint = 3,
    chest = 4,
    essenceGenerator = 5,
}

public class TileScript : MonoBehaviour {


    public int XCoord;
    public int YCoord;
    public bool isWalkable;
    public bool NEWall;
    public bool NWWall;



    Color previousColor;
    List<Node> walkPath = new List<Node>();

    // Use this for initialization
    void Start ()
    {
     
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseEnter()
    {
        /*
        if (isWalkable)
        {
            previousColor = this.GetComponent<SpriteRenderer>().color;
            this.GetComponent<SpriteRenderer>().color = Color.magenta;

            WizardScript wizard = transform.parent.parent.GetChild(1).GetChild(1).GetComponent("WizardScript") as WizardScript;

            walkPath = Pathfinding.findPathToDestination(wizard.currentTilePositionX, wizard.currentTilePositionY, XCoord, YCoord);
            foreach(Node node in walkPath)
            {
                BoardScript.Tiles[node.X, node.Y].GetComponent<SpriteRenderer>().color = Color.yellow;
            }

        }
        */
    }

    void OnMouseExit()
    {
        /*
        if (isWalkable)
        {
            this.GetComponent<SpriteRenderer>().color = previousColor;
        }

        foreach (Node node in walkPath)
        {
            BoardScript.Tiles[node.X, node.Y].GetComponent<SpriteRenderer>().color = Color.white;
        }
        */
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
            case TileType.essenceGenerator:
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
        NEWall = tileProp.NEWall;
        NWWall = tileProp.NWWall;
    }
}
