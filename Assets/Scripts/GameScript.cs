using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {

    int playersTurn;
    int firstPlayer;
    public GameObject currentWizard;

    public GameObject wizard1;
    public GameObject wizard2;
    public GameObject wizard3;
    public GameObject wizard4;

    // Use this for initialization
    void Start () {
        playersTurn = 1;
        currentWizard = wizard1;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void NextTurn ()
    {
        playersTurn++;
        if (playersTurn > 16)
        {
            playersTurn = 1;            
        }

        int wizardTurn = 0;
        
        if (playersTurn == 1 || playersTurn == 8 || playersTurn == 11 || playersTurn == 14)
        {
            wizardTurn = 1;
        }
        else if (playersTurn == 2 || playersTurn == 5 || playersTurn == 12 || playersTurn == 15)
        {
            wizardTurn = 2;
        }
        else if (playersTurn == 3 || playersTurn == 6 || playersTurn == 9 || playersTurn == 16)
        {
            wizardTurn = 3;
        }
        else if (playersTurn == 4 || playersTurn == 7 || playersTurn == 10 || playersTurn == 13)
        {
            wizardTurn = 4;
        }

        switch (wizardTurn)
        {
            case 1:
                GameObject.Find("Canvas").GetComponent<UserInterfaceScript>().wizard = wizard1;
                currentWizard = wizard1;
                break;
            case 2:
                GameObject.Find("Canvas").GetComponent<UserInterfaceScript>().wizard = wizard2;
                currentWizard = wizard2;
                break;
            case 3:
                GameObject.Find("Canvas").GetComponent<UserInterfaceScript>().wizard = wizard3;
                currentWizard = wizard3;
                break;
            case 4:
                GameObject.Find("Canvas").GetComponent<UserInterfaceScript>().wizard = wizard4;
                currentWizard = wizard4;
                break;
        }
    }
}
