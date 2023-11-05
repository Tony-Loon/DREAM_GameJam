using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSwitch : Interactable
{
    [Header ("Lamps that turn on when power switch is enabled")]
    public List<Light> AllLights;
    public List<float> _intensities;
    [Header ("SFX that play when power switch is enabled")]
    public List<AudioSource> SFXAudioSources;
    public bool PowerOn = false;
    public float LightUpSpeed = 10.0f;

    public void Start()
    {
        for (int i = 0; i < AllLights.Count; i++)
        {
            _intensities.Add(AllLights[i].intensity);
        }
        TurnPowerOff();
    }

    public void Update()
    {
        if(!PowerOn && AllLights[0].intensity > 0) { TurnPowerOff(); }
        else if(PowerOn && AllLights[0].intensity < _intensities[0]) { TurnPowerOn(); }
    }
    public override void Interaction()
    {
    }

    public override void OnInteractionStart()
    {
        Debug.Log("Hold switch!");
        foreach(AudioSource audio in SFXAudioSources)
        {
            audio.PlayOneShot(audio.clip);
        }
        if(!PowerOn)
        {
            PowerOn = true;
        }
        else if(PowerOn) 
        { 
            PowerOn = false;
        }
    }

    public override void OnInteractionStop()
    {
        Debug.Log("Let switch go!");
    }

    void TurnPowerOn() 
    {
        Debug.Log("Lights on!");
        for (int i = 0; i < AllLights.Count; i++)
        {
            if (AllLights[i].intensity < _intensities[i])
            {
                AllLights[i].intensity += Time.deltaTime * LightUpSpeed;
            }
        }
    }
    void TurnPowerOff()
    {
        Debug.Log("Lights off!");
        for (int i = 0; i < AllLights.Count; i++)
        {
            AllLights[i].intensity = 0.0f;
        }
    }
}