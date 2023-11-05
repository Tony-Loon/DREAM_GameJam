using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractableSwitch : Interactable
{
    [Header ("Lamps that turn on when power switch is enabled")]
    public List<Light> AllLights;
    public List<float> _intensities;
    public Material LightBoxes, Noise;
    [Header ("SFX that play when power switch is enabled")]
    public List<AudioSource> SFXAudioSources;
    public List<GameObject> Monitor;
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
        else if(PowerOn && AllLights[0].intensity <= _intensities[0]) { TurnPowerOn(); }
    }
    public override void Interaction()
    {
    }

    public override void OnInteractionStart()
    {
        Debug.Log("Hold switch!");
        LightBoxes.EnableKeyword("_EMISSION");
        LightBoxes.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
        LightBoxes.SetColor("_EmissionColor", new Color(0.749f,0.5852f,0.3003f,1f));
        Noise.SetColor("_Color", new Color(0.7877358f, 1f, 0.9505f,1f));
        Monitor[0].SetActive(false);
        for (int i = 1; i < Monitor.Count; i++)
        {
            if (i == 3)
            {
                Monitor[2].SetActive(false);
            }
            TurnDisplaysOn(Monitor[i], 1000*(i+1));
        }
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
        Noise.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
    }

    private async void TurnDisplaysOn(GameObject obj, int delay)
    {
        await Task.Delay(delay);
        obj.SetActive(true);
    }
}