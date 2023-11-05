using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderReader : MonoBehaviour
{
    Collider curCollider;

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.gameObject.GetComponent<IInteractable>();

        if (interactable == null)
        {
            IInspactable inspactable = other.gameObject.GetComponent<IInspactable>();
            if (inspactable != null)
            {
                curCollider = other;
            }
        }
        else
        {
            {
                curCollider = other;
            }
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
