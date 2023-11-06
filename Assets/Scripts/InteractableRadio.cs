using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRadio : Interactable
{
    public AudioClip SFX;
    private AudioSource _audioSource;
    private Monitor_Order _order;

    public void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _order = GameObject.Find("MonitorDisplay").GetComponent<Monitor_Order>();
    }

    public override void Interaction()
    {
    }

    public override void OnInteractionStart()
    {
        Debug.Log("Turned Radio on!");
        if (SFX)  { _audioSource.PlayOneShot(SFX); }
        _audioSource.Play();
        _order._radioOn = true;
        _order.ClosePopUp(_order.Pop_Up3, 3, true);
    }

    public override void OnInteractionStop()
    {
        Debug.Log("Let go off Radio!");
    }
}