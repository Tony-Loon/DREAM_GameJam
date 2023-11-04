using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable {
    public void Interaction();
    public void OnInteractionStart();
    public void OnInteractionStop();
}
