using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableBook : Interactable, ICarry
{
    public TextMeshProUGUI Tooltip;
    private GameObject _hands;
    public GameObject Book;
    public int _pickedUp = 0;
    public bool _blockingVent = true;

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
        if (_pickedUp < 2)
        {
            Debug.Log("Picked up Book!");
            _pickedUp++;
        }
    }
    public void Carrying() {
        if (_pickedUp < 2)
        {
            Book.transform.position = _hands.transform.position + new Vector3(0,1,0);
            Book.transform.rotation = _hands.transform.rotation;
        }
    }

    public void LayDown() {
        Tooltip.text = "Pick up";
        Book.GetComponent<Collider>().enabled = true;
        Book.GetComponent<Rigidbody>().useGravity = true; 
        if (_pickedUp <= 2)
        {
            Debug.Log("Dropped Book!");
            _pickedUp = 0;
        }
    }


}