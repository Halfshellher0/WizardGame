using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardScript : MonoBehaviour {

    //wizard specific parameters
    public float wizardMoveSpeed = 1.0f;
    public int currentTilePositionX = 0;
    public int currentTilePositionY = 0;
    public bool randomlyMove;
    float wizardTileOffsetX = 0.01f;
    float wizardTileOffsetY = 0.45f;

    //program varibles
    float t;
    float r;
    Vector3 startPosition;
    Vector3 target;
    float timeToReachTarget;
    bool moving;
    Direction movingDirection;
    List<Node> currentWalkingPath;
    int currentLayer;

    //Game Variables
    public int health;
    public int movePoints;
    public int actionPoints;
    public int victoryPoints;
    public int fireEssence;
    public int waterEssence;
    public int natureEssence;
    public int voidEssence;



    // Use this for initialization
    void Start () {
        //initialize Game variables;
        health = 5;
        movePoints = 0;
        actionPoints = 3;
        victoryPoints = 0;
        fireEssence = 0;
        waterEssence = 0;
        natureEssence = 0;
        voidEssence = 0;

        moving = false;
        float[] tileCoords = gridPositionToScreenPosition(currentTilePositionX, currentTilePositionY);
        currentLayer = 4 * (currentTilePositionX + currentTilePositionY) + 3;
        transform.position = new Vector3(tileCoords[0], tileCoords[1], 0);
        GetComponent<SpriteRenderer>().sortingOrder = currentLayer;

    }
	
	// Update is called once per frame
	void Update () {

        if (moving)
        {
            if (t <= 1.0f)
            {
                t += Time.deltaTime / timeToReachTarget;
                transform.position = Vector3.Lerp(startPosition, target, t);
            }
            else
            {
                moving = false;
                if (!randomlyMove)
                {
                    walkOnPath(currentWalkingPath);
                }
            }
        }
        else
        {
            if (randomlyMove)
            {
                r = Random.value;
                if (r < 0.25)
                {
                    MoveDirection(Direction.northEast);
                }
                else if (r < 0.50)
                {
                    MoveDirection(Direction.northWest);
                }
                else if (r < 0.75)
                {
                    MoveDirection(Direction.southEast);
                }
                else
                {
                    MoveDirection(Direction.southWest);
                }
            }
        }
        
    }

    public void walkOnPath(List<Node> walkPath)
    {
        currentWalkingPath = walkPath;
        if (walkPath.Count > 0 && walkPath.Count <= movePoints + 1)
        {            
            //Check if wizard is at start of path.
            if (currentTilePositionX == walkPath[0].X && currentTilePositionY == walkPath[0].Y)
            {
                currentWalkingPath.RemoveAt(0);

                if (currentWalkingPath.Count > 0)
                {
                    if (!moving)
                    {
                        if (currentTilePositionX < walkPath[0].X && currentTilePositionY == walkPath[0].Y)
                        {                            
                            //walk SouthEast
                            MoveDirection(Direction.southEast);
                            movePoints--;
                        }
                        else if (currentTilePositionX == walkPath[0].X && currentTilePositionY < walkPath[0].Y)
                        {
                            
                            //walk SouthWest
                            MoveDirection(Direction.southWest);
                            movePoints--;
                        }
                        else if (currentTilePositionX > walkPath[0].X && currentTilePositionY == walkPath[0].Y)
                        {
                            //walk NorthWest
                            MoveDirection(Direction.northWest);
                            movePoints--;
                        }
                        else if (currentTilePositionX == walkPath[0].X && currentTilePositionY > walkPath[0].Y)
                        {
                            //walk NorthEast
                            MoveDirection(Direction.northEast);
                            movePoints--;
                        }

                    }

                }
            }
        }
    }

    float[] gridPositionToScreenPosition(int x, int y)
    {
        float[] returnFloat = new float[2] { 0f, 0f };

        returnFloat[0] = ((float)y * -0.64f) + ((float)x * 0.64f) + wizardTileOffsetX;
        returnFloat[1] = ((float)(x + y) * -0.32f)+ wizardTileOffsetY;

        return returnFloat;
    }

    //Move to this grid position, in X amount of seconds;
    void MoveToPosition (int x, int y, float time)
    {
        if (!moving)
        {
            t = 0;
            float[] tileCoords = gridPositionToScreenPosition(x, y);
            startPosition = transform.position;
            timeToReachTarget = time / wizardMoveSpeed;
            target = new Vector3(tileCoords[0], tileCoords[1], 0);
            currentTilePositionX = x;
            currentTilePositionY = y;
            Debug.Log("(" + x + "," + y + ")");
            moving = true;
        }
    }

    //Move one tile in a given direction
    void MoveDirection (Direction d)
    {
        int targetX;
        int targetY;
        bool targetTileWalkable;

        switch (d)
        {
            case Direction.northEast:
                targetX = currentTilePositionX;
                targetY = currentTilePositionY - 1;
                if (targetY > -1)
                {
                    targetTileWalkable = BoardScript.Tiles[targetX, targetY].GetComponent<TileScript>().isWalkable && !BoardScript.Tiles[currentTilePositionX, currentTilePositionY].GetComponent<TileScript>().NEWall;
                    if (targetTileWalkable)
                    {
                        MoveToPosition(targetX, targetY, 1f);
                        movingDirection = Direction.northEast;
                        currentLayer -= 4;
                        GetComponent<SpriteRenderer>().sortingOrder = currentLayer;
                    }
                }
                break;
            case Direction.northWest:
                targetX = currentTilePositionX - 1;
                targetY = currentTilePositionY;
                if (targetX > -1)
                {
                    targetTileWalkable = BoardScript.Tiles[targetX, targetY].GetComponent<TileScript>().isWalkable && !BoardScript.Tiles[currentTilePositionX, currentTilePositionY].GetComponent<TileScript>().NWWall; 
                    if (targetTileWalkable)
                    {
                        MoveToPosition(targetX, targetY, 1f);
                        movingDirection = Direction.northWest;
                        currentLayer -= 4;
                        GetComponent<SpriteRenderer>().sortingOrder = currentLayer;
                    }
                }
                break;
            case Direction.southEast:
                targetX = currentTilePositionX + 1;
                targetY = currentTilePositionY;
                if (targetX < GlobalGameParameters.maxBoardHeight)
                {
                    targetTileWalkable = BoardScript.Tiles[targetX, targetY].GetComponent<TileScript>().isWalkable && !BoardScript.Tiles[targetX, targetY].GetComponent<TileScript>().NWWall;

                    if (targetTileWalkable)
                    {
                        MoveToPosition(targetX, targetY, 1f);
                        movingDirection = Direction.southEast;
                        currentLayer += 4;
                        GetComponent<SpriteRenderer>().sortingOrder = currentLayer;
                    }
                }
                break;
            case Direction.southWest:
                targetX = currentTilePositionX;
                targetY = currentTilePositionY + 1;
                if (targetY < GlobalGameParameters.maxBoardWidth)
                {
                    targetTileWalkable = BoardScript.Tiles[targetX, targetY].GetComponent<TileScript>().isWalkable && !BoardScript.Tiles[targetX, targetY].GetComponent<TileScript>().NEWall;
                    if (targetTileWalkable)
                    {
                        MoveToPosition(targetX, targetY, 1f);
                        movingDirection = Direction.southWest;
                        currentLayer += 4;
                        GetComponent<SpriteRenderer>().sortingOrder = currentLayer;
                    }
                }
                break;
        }
        
    }
}
