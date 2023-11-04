using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTV : Interactable
{

    public override void Interaction()
    {
        // nothig yet ;-;
    }

    public override void OnInteractionStart()
    {
        Debug.Log("Move to tv screen");
    }

    public override void OnInteractionStop()
    {
        Debug.Log("Move away from tv screen");
    }
}
