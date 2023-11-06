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
    public Material LightBoxes;
    [Header ("SFX that play when power switch is enabled")]
    public List<AudioSource> SFXAudioSources;
    private Monitor_Order _order;
    public bool PowerOn = false;
    public float LightUpSpeed = 10.0f;

    public void Start()
    {
        for (int i = 0; i < AllLights.Count; i++)
        {
            _intensities.Add(AllLights[i].intensity);
        }
        TurnPowerOff();
        _order = GameObject.Find("MonitorDisplay").GetComponent<Monitor_Order>();
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
        if (!PowerOn)
        {
            Debug.Log("POWER ON!");
            PowerOn = true;
            _order.ActivateDisplays();
            LightBoxes.EnableKeyword("_EMISSION");
            LightBoxes.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
            LightBoxes.SetColor("_EmissionColor", new Color(0.749f,0.5852f,0.3003f,1f));
            foreach (AudioSource audio in SFXAudioSources)
            {
                audio.PlayOneShot(audio.clip);
            }
        }
        else if(PowerOn)
        {
            Debug.Log("POWER OFF!");
            PowerOn = false;
            _order.ShutDown();
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