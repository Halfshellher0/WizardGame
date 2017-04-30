using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceScript : MonoBehaviour {

    public GameObject wizard;
    WizardScript wizardScript;
    public Sprite emptyActionPoint;
    public Sprite fullActionPoint;

    // Use this for initialization
    void Start () {
        wizardScript = wizard.GetComponent<WizardScript>();
    }
	
	// Update is called once per frame
	void Update () {

        Transform child;
        Text t;
        Image image;
        wizardScript = wizard.GetComponent<WizardScript>();

        //Update Health Display
        child = transform.Find("out_Health");
        t = child.GetComponent<Text>();
        t.text = wizardScript.health.ToString();

        //Update Movement Points Display
        child = transform.Find("out_MovementPoints");
        t = child.GetComponent<Text>();
        t.text = wizardScript.movePoints.ToString();

        //Update Movement Points Display
        child = transform.Find("out_MovementPoints");
        t = child.GetComponent<Text>();
        t.text = wizardScript.movePoints.ToString();
        
        //ActionPoint Image1
        child = transform.Find("img_ActionPoint1");
        image = child.GetComponent<Image>();
        if (wizardScript.actionPoints > 2)
        {
            image.sprite = fullActionPoint;
        }
        else
        {
            image.sprite = emptyActionPoint;
        }

        //ActionPoint Image2
        child = transform.Find("img_ActionPoint2");
        image = child.GetComponent<Image>();
        if (wizardScript.actionPoints > 1)
        {
            image.sprite = fullActionPoint;
        }
        else
        {
            image.sprite = emptyActionPoint;
        }

        //ActionPoint Image3
        child = transform.Find("img_ActionPoint3");
        image = child.GetComponent<Image>();
        if (wizardScript.actionPoints > 0)
        {
            image.sprite = fullActionPoint;
        }
        else
        {
            image.sprite = emptyActionPoint;
        }

        //Wizard Image
        child = transform.Find("img_Wizard");
        image = child.GetComponent<Image>();        
        image.sprite = wizard.GetComponent<SpriteRenderer>().sprite;
        image.color = wizard.GetComponent<SpriteRenderer>().color;





    }

    //Code for the move Button.
    public void btn_Move_Click()
    {
        wizardScript = wizard.GetComponent<WizardScript>();
        if (wizardScript.actionPoints > 0)
        {
            //Add Movement Points
            wizardScript.movePoints += 25;
            wizardScript.actionPoints--;
        }
    }

    //Code for the Cast Minor Spell Button.
    public void btn_CastMinor_Click()
    {
        wizardScript = wizard.GetComponent<WizardScript>();
        if (wizardScript.actionPoints > 0)
        {
            //Cast Minor Spell
            wizardScript.actionPoints--;
        }
    }

    //Code for the Cast Major Spell Button.
    public void btn_CastMajor_Click()
    {
        wizardScript = wizard.GetComponent<WizardScript>();
        if (wizardScript.actionPoints > 1)
        {
            //Cast Major Spell
            wizardScript.actionPoints -= 2;
        }
    }

    //Code for the Learn Minor Spell Button.
    public void btn_LearnMinor_Click()
    {
        wizardScript = wizard.GetComponent<WizardScript>();
        if (wizardScript.actionPoints > 1)
        {
            //Learn Minor Spell            
            wizardScript.actionPoints -= 2;
        }
    }

    //Code for the Learn Major Spell Button.
    public void btn_LearnMajor_Click()
    {
        wizardScript = wizard.GetComponent<WizardScript>();
        if (wizardScript.actionPoints > 2)
        {
            //Learn Major Spell            
            wizardScript.actionPoints -= 3;
        }
    }

    //Code for the Learn Major Spell Button.
    public void btn_EndTurn_Click()
    {
        wizardScript = wizard.GetComponent<WizardScript>();       
        GameObject.Find("Board").SendMessage("NextTurn");
        wizardScript.actionPoints = 3;
        wizardScript.movePoints = 0;
    }
}
