using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSwitch : Interactable
{
    public Light light1, light2;
    //public GameObject TV;
    public override void Interaction()
    {
        // nothig yet ;-;
    }

    public override void OnInteractionStart()
    {
        Debug.Log("Activate Power Switch");
    }

    public override void OnInteractionStop()
    {
        Debug.Log("Power fully activated!");
    }

}