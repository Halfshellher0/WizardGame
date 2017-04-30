using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{    
    public int X;
    public int Y;
    public int G; //Movement amount from origin position.
    public int Score;
    public Node ParentNode;

    public Node(int x, int y, Node parentNode, int H)
    {        
        X = x;
        Y = y;
        ParentNode = parentNode;

        if (parentNode == null)
        {
            G = 0;
        }
        else
        {
            G = parentNode.G + 1;
        }

        Score = G + H;
    }
}



static public class Pathfinding  {


    //Finds the path to the destination tile
    static public List<Node> findPathToDestination(int sourceX, int sourceY, int destinationX, int destinationY)
    {
        List<Node> pathToDest = new List<Node>();
        List<Node> openList = new List<Node>();
        List<Node> tempList = new List<Node>();       
        List<Node> closedList = new List<Node>();        
        bool finishLoop = false;
        bool foundDestination = false;

        Node currentNode = new Node(sourceX, sourceY, null, calculateH(sourceX, sourceY, destinationX, destinationY));
        tempList = retieveAdjacentWalkableNodes(currentNode, closedList, destinationX, destinationY);

        foreach (Node node in tempList)
        {
            openList.Add(node);
        }
        tempList.Clear();

        closedList.Add(currentNode);

        while (!finishLoop)
        {
            if (openList.Count > 0)
            {
                //find the node with the worst score, move it to closed list.
                int removeIndex = findLowestScore(openList);
                if (openList[removeIndex].X == destinationX && openList[removeIndex].Y == destinationY)
                {
                    finishLoop = true;
                    foundDestination = true;
                }

                currentNode = openList[removeIndex];
                closedList.Add(currentNode);                
                openList.RemoveAt(removeIndex);
            }
            else
            {
                finishLoop = true;
            }
            

            if (!finishLoop)
            {
                
                tempList = retieveAdjacentWalkableNodes(currentNode, closedList, destinationX, destinationY);               
                

                //If the Node already exists in openList, check to see if its a shorter path, otherwise remove it.
                int lineCounter = 0;
                foreach (Node node in tempList)
                {
                    if (isNodeInList(tempList[lineCounter].X, tempList[lineCounter].Y, openList))
                    {
                        int openListIndex = findNodeInList(tempList[lineCounter].X, tempList[lineCounter].Y, openList);
                        if (tempList[lineCounter].G < openList[openListIndex].G)
                        {
                            openList.RemoveAt(openListIndex);
                            openList.Add(tempList[lineCounter]);
                        }
                    }
                    else
                    {
                        openList.Add(node);
                    }
                    lineCounter++;
                }
                tempList.Clear();
            }
        }

        if (foundDestination)
        {
            pathToDest.Add(closedList[closedList.Count - 1]);
            Node parentNode = pathToDest[0].ParentNode;
            while (parentNode != null)
            {
                pathToDest.Add(parentNode);
                parentNode = parentNode.ParentNode;
            }
        }

        pathToDest.Reverse();
        return pathToDest;
    }

    //Returns a list of all adjacent tiles to the searchNode that are walkable, also checks that they are not already in the closed list;
    static List<Node> retieveAdjacentWalkableNodes(Node searchNode, List<Node> closedList, int destinationX, int destinationY)
    {
        List<Node> returnNodes = new List<Node>();
        int targetX;
        int targetY;

        //Check if tiles are walkable, if so create a node for them and add it to the list.
        
        //NorthEast Node
        targetX = searchNode.X;
        targetY = searchNode.Y - 1;
        if (targetY > -1)
        {
            if(BoardScript.Tiles[targetX, targetY].GetComponent<TileScript>().isWalkable && !BoardScript.Tiles[searchNode.X, searchNode.Y].GetComponent<TileScript>().NEWall)
            {
                if (!isNodeInList(targetX, targetY, closedList))
                {                    
                    Node newNode = new Node(targetX, targetY, searchNode, calculateH(targetX, targetY, destinationX, destinationY));
                    returnNodes.Add(newNode);                 
                }
            }
        }

        //NorthWest Node
        targetX = searchNode.X - 1;
        targetY = searchNode.Y;
        if (targetX > -1)
        {
            if (BoardScript.Tiles[targetX, targetY].GetComponent<TileScript>().isWalkable && !BoardScript.Tiles[searchNode.X, searchNode.Y].GetComponent<TileScript>().NWWall)
            {
                if (!isNodeInList(targetX, targetY, closedList))
                {                    
                    Node newNode = new Node(targetX, targetY, searchNode, calculateH(targetX, targetY, destinationX, destinationY));
                    returnNodes.Add(newNode);                       
                }
            }
        }

        //SouthWest Node
        targetX = searchNode.X;
        targetY = searchNode.Y + 1;
        if (targetY < GlobalGameParameters.maxBoardHeight)
        {
            if (BoardScript.Tiles[targetX, targetY].GetComponent<TileScript>().isWalkable && !BoardScript.Tiles[targetX, targetY].GetComponent<TileScript>().NEWall)
            {
                if (!isNodeInList(targetX, targetY, closedList))
                {                    
                    Node newNode = new Node(targetX, targetY, searchNode, calculateH(targetX, targetY, destinationX, destinationY));
                    returnNodes.Add(newNode);                     
                }
            }
        }

        //SouthEast Node
        targetX = searchNode.X + 1;
        targetY = searchNode.Y;
        if (targetX < GlobalGameParameters.maxBoardWidth)
        {
            if (BoardScript.Tiles[targetX, targetY].GetComponent<TileScript>().isWalkable && !BoardScript.Tiles[targetX, targetY].GetComponent<TileScript>().NWWall)
            {
                if (!isNodeInList(targetX, targetY, closedList))
                {                   
                    Node newNode = new Node(targetX, targetY, searchNode, calculateH(targetX, targetY, destinationX, destinationY));
                    returnNodes.Add(newNode);                      
                }
            }
        }

        return returnNodes;
    }

    //function for calculating rough estimation of move amounts required to desination
    static int calculateH(int sourceX, int sourceY, int destinationX, int destinationY)
    {
        return Mathf.Abs(sourceX - destinationX) + Mathf.Abs(sourceY - destinationY); 
    }

    //return the index of the lowest Scored node in the openList
    static int findLowestScore(List<Node> openList)
    {
        int returnInt = 0;
        int lowestScore = 999999;
        int lineCounter = 0;
        foreach (Node node in openList)
        {
            if (node.Score < lowestScore)
            {
                lowestScore = node.Score;
                returnInt = lineCounter;
            }
            lineCounter++;
        }
        return returnInt;
    }

    //return the index of the node in openList
    static int findNodeInList(int x, int y, List<Node> nodeList)
    {     
        int lineCounter = 0;
        foreach (Node node in nodeList)
        {
            if (node.X == x && node.Y == y)
            {                
                break;
            }
            lineCounter++;
        }
        return lineCounter;
    }

    //Checks if the Node at coord (x,y) is in the closedList
    static bool isNodeInList(int x, int y, List<Node> nodeList)
    {
        bool returnBool = false;
        foreach (Node node in nodeList)
        {
            if (node.X == x && node.Y == y)
            {
                returnBool = true;
                break;
            }
        }
        return returnBool;
    }
}
