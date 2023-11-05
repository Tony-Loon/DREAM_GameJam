using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableRoomba : Interactable, ICarry
{
    public TextMeshProUGUI Tooltip;
    private GameObject _hands;
    public GameObject  Roomba, PopUp;
    public bool _pickedUp = false;

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

    public void PickUp()
    {
        Tooltip.text = "Lay down";
        Roomba.GetComponent<Collider>().enabled = false;
        Roomba.GetComponent<Rigidbody>().useGravity = false;
        _hands = GameObject.Find("hands_Cylinder.045");
        if (!_pickedUp)
        {
            Debug.Log("Picked up Roomba!");
            _pickedUp = true;
        }
    }
    public void Carrying()
    {
        if (_pickedUp)
        {
            Roomba.transform.position = _hands.transform.position + new Vector3(0, 1, 0);
            Roomba.transform.rotation = _hands.transform.rotation;
        }
    }

    public void LayDown()
    {
        Tooltip.text = "Pick up";
        Roomba.GetComponent<Collider>().enabled = true;
        Roomba.GetComponent<Rigidbody>().useGravity = true;
        if (_pickedUp)
        {
            Debug.Log("Dropped Roomba!");
            _pickedUp = false;
        }
        PopUp.SetActive(false);
    }
}