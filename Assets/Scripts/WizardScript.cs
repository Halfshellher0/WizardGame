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


    // Use this for initialization
    void Start () {
        moving = false;
        float[] tileCoords = gridPositionToScreenPosition(currentTilePositionX, currentTilePositionY);
        transform.position = new Vector3(tileCoords[0], tileCoords[1], 0);        
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

    float[] gridPositionToScreenPosition(int x, int y)
    {
        float[] returnFloat = new float[2] { 0f, 0f };

        returnFloat[0] = ((float)y * -0.64f) + ((float)x * 0.64f) + wizardTileOffsetX;
        returnFloat[1] = ((float)(x + y) * 0.32f) - 6.08f + wizardTileOffsetY;

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
                targetX = currentTilePositionX + 1;
                targetY = currentTilePositionY;
                targetTileWalkable = BoardScript.Tiles[targetX, targetY].GetComponent<TileScript>().isWalkable;
                if (targetX < GlobalGameParameters.maxBoardWidth && targetTileWalkable)
                {
                    MoveToPosition(targetX, targetY, 1f);
                    movingDirection = Direction.northEast;
                }
                break;
            case Direction.northWest:
                targetX = currentTilePositionX;
                targetY = currentTilePositionY + 1;
                targetTileWalkable = BoardScript.Tiles[targetX, targetY].GetComponent<TileScript>().isWalkable;
                if (targetY < GlobalGameParameters.maxBoardHeight && targetTileWalkable)
                {
                    MoveToPosition(targetX, targetY, 1f);
                    movingDirection = Direction.northWest;
                }
                break;
            case Direction.southEast:
                targetX = currentTilePositionX;
                targetY = currentTilePositionY - 1;
                targetTileWalkable = BoardScript.Tiles[targetX, targetY].GetComponent<TileScript>().isWalkable;
                if (targetY > -1 && targetTileWalkable)
                {
                    MoveToPosition(targetX, targetY, 1f);
                    movingDirection = Direction.southEast;
                }
                break;
            case Direction.southWest:
                targetX = currentTilePositionX - 1;
                targetY = currentTilePositionY;
                targetTileWalkable = BoardScript.Tiles[targetX, targetY].GetComponent<TileScript>().isWalkable;
                if (targetX > -1 && targetTileWalkable)
                {
                    MoveToPosition(targetX, targetY, 1f);
                    movingDirection = Direction.southWest;
                }
                break;
        }
        
    }
}
