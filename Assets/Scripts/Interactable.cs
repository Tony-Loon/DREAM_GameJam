using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] protected Canvas canvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canvas?.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canvas?.gameObject.SetActive(false);
        }
    }

    public virtual void Interaction()
    {
        // nothig yet ;-;
    }

    public virtual void OnInteractionStart()
    {
        Debug.Log("Interaction Started");
    }

    public virtual void OnInteractionStop()
    {
        Debug.Log("Interaction Stopped");
    }
}
