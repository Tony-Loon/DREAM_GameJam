using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderReader : MonoBehaviour
{
    Collider curCollider;

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.gameObject.GetComponent<IInteractable>();

        if (interactable != null)
        {
            curCollider = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == curCollider)
            curCollider = null;
    }

    public Collider getCurrentCollider()
    {
        if (curCollider == null)
        {
            Debug.Log("Oh no, there is currently no collider owo");
        }
        return curCollider;
    }
}
