using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableBook : Interactable, ICarry
{
    public TextMeshProUGUI Tooltip;
    private GameObject _hands;
    public GameObject Book, PopUp;
    public bool _blockingVent = true, _pickedUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canvas?.gameObject.SetActive(true);
        }
        if (other.gameObject.name == "Vent")
        {
            Debug.Log("Vent still blocked ):");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canvas?.gameObject.SetActive(false);
        }
        if (other.gameObject.name == "Vent")
        {
            Debug.Log("Vent freeeee :3"); 
        }
    }

    public void PickUp() {
        Tooltip.text = "Lay down";
        Book.GetComponent<Collider>().enabled = false;
        Book.GetComponent<Rigidbody>().useGravity = false; 
        _hands = GameObject.Find("hands_Cylinder.045");
        
        if (!_pickedUp)
        {
            Debug.Log("Picked up Book!");
            _pickedUp= true;
        }
    }
    public void Carrying() {
        if(_pickedUp)
        {
            Book.transform.position = _hands.transform.position + new Vector3(0, 1, 0);
            Book.transform.rotation = _hands.transform.rotation;
        }
    }

    public void LayDown()
    {
        Tooltip.text = "Pick up";
        Book.GetComponent<Collider>().enabled = true;
        Book.GetComponent<Rigidbody>().useGravity = true;
        if (_pickedUp)
        {
            Debug.Log("Dropped Book!");
            _pickedUp = false;
        }
        if (!_blockingVent)
        {
            PopUp.SetActive(false);
        }
    }
}