using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileClick : MonoBehaviour, IPointerClickHandler {	

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            WizardScript wizard = GameObject.Find("Board").GetComponent<GameScript>().currentWizard.GetComponent<WizardScript>();
            List<Node> walkPath = Pathfinding.findPathToDestination(wizard.currentTilePositionX, wizard.currentTilePositionY, transform.GetComponent<TileScript>().XCoord, transform.GetComponent<TileScript>().YCoord);
            wizard.walkOnPath(walkPath);
        }
    }
}
