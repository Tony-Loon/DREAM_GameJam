using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRadio : Interactable
{
    public AudioClip SFX;
    private AudioSource _audioSource;

    public void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public override void Interaction()
    {
    }

    public override void OnInteractionStart()
    {
        Debug.Log("Turned Radio on!");
        if (SFX)  { _audioSource.PlayOneShot(SFX); }
        _audioSource.Play();
    }

    public override void OnInteractionStop()
    {
        Debug.Log("Let go off Radio!");
    }
}