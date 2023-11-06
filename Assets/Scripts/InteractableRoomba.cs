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
    public Monitor_Order _order;
    public bool _pickedUp = false;

    void Start()
    {
        _order = GameObject.Find("MonitorDisplay").GetComponent<Monitor_Order>();
    }
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
        _order._roombaFree = true;
        _order.ClosePopUp(_order.Pop_Up1, 3, true);
    }
}