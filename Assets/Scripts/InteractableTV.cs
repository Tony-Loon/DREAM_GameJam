using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class InteractableTV : Interactable
{
    [SerializeField]CinemachineFreeLook cineLook;
    Transform oldTarget;
    Transform oldLookAt;
    
    public override void Interaction()
    {
        // nothing yet ;-;
    }

    public override void OnInteractionStart()
    {
        Debug.Log("Move to tv screen");
        oldLookAt = cineLook.LookAt;
        oldTarget = cineLook.Follow;
        cineLook.LookAt = this.gameObject.transform;
        cineLook.Follow = this.gameObject.transform;
    }

    public override void OnInteractionStop()
    {
        Debug.Log("Move away from tv screen");
        cineLook.LookAt = oldLookAt;
        cineLook.Follow = oldTarget;
    }
}
