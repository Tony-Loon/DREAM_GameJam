using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSwitch : Interactable
{
    [Header ("Lamps that turn on when power switch is enabled")]
    public List<Light> AllLights;
    public List<float> _intensities;
    public Material LightBoxes;
    [Header ("SFX that play when power switch is enabled")]
    public List<AudioSource> SFXAudioSources;
    public bool PowerOn = false;
    public float LightUpSpeed = 10.0f;

    public void Start()
    {
        for (int i = 0; i < AllLights.Count; i++)
        {
            _intensities.Add(AllLights[i].intensity);
            AllLights[i].enabled = false;
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
        LightBoxes.EnableKeyword("_EMISSION");
        LightBoxes.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
        LightBoxes.SetColor("_EmissionColor", Color.yellow);
        foreach (AudioSource audio in SFXAudioSources)
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
        LightBoxes.DisableKeyword("_EMISSION");
        LightBoxes.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
        LightBoxes.SetColor("_EmissionColor", Color.black);
        for (int i = 0; i < AllLights.Count; i++)
        {
            AllLights[i].intensity = 0.0f;
        }
    }
}