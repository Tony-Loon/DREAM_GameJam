using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Monitor_Order : MonoBehaviour
{
    public GameObject EyeClosed, EyeOpen, EyeIdle, Pop_Up1, Pop_Up2, Pop_Up3,TextBox;
    private List<GameObject> _displays;
    private List<float> _delays = new List<float>{1,2,1,2,2,2};
    public TextMeshProUGUI Text;
    public Material Noise, HappyDream;
    private AudioSource _audioSource;
    public AudioClip PopUp, PopDown;
    public bool _initialPowerOn = true, _PowerOn = true, _radioOn = false, _roombaFree = false, _ventUnblocked = false;
    public int _index = 0;

    public void Start()
    {
        _displays = new List<GameObject>();
        EyeClosed = transform.GetChild(1).gameObject;
        EyeOpen = transform.GetChild(2).gameObject;
        EyeIdle = transform.GetChild(3).gameObject; 
        Pop_Up1 = transform.GetChild(4).gameObject;
        Pop_Up2 = transform.GetChild(5).gameObject;
        Pop_Up3 = transform.GetChild(6).gameObject;
        TextBox = transform.GetChild(7).gameObject;
        _displays.Add(EyeOpen);
        _displays.Add(EyeIdle);
        _displays.Add(TextBox);
        _displays.Add(Pop_Up3);
        _displays.Add(Pop_Up2);            
        _displays.Add(Pop_Up1);

        Text = TextBox.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void ActivateDisplays()
    {   
        Noise.SetColor("_Color", new Color(0.7877358f, 1f, 0.9505f, 1f));
        EyeClosed.SetActive(false);
        if (_index < _displays.Count)
        {
            if (_displays[_index].name == "Pop_Up1" && !_initialPowerOn && _roombaFree)
            {
                _displays[_index].SetActive(false);
                _index++;
                ActivateDisplays();
            }
            else if (_displays[_index].name == "Pop_Up2" && !_initialPowerOn && _ventUnblocked)
            {
                _displays[_index].SetActive(false);
                _index++;
                ActivateDisplays();
            }
            else if (_displays[_index].name == "Pop_Up3" && !_initialPowerOn && _radioOn)
            {
                _displays[_index].SetActive(false);
                _index++;
                ActivateDisplays();
            }
            else { 
                _displays[_index].SetActive(true);
                Debug.Log(_displays[_index].name);
                StartCoroutine(DeactivateDisplayWithDelay(_delays[_index]));
            }
        }
        else
        {
            _initialPowerOn = false;
        }
    }

    private IEnumerator DeactivateDisplayWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(_displays[_index].name == "EyeOpen" )
        {
            _displays[_index].SetActive(false);
        }
        _index++;
        if (_index == 2 && !_initialPowerOn)
        {
            _index++;
        }
        ActivateDisplays();
    }

    public void ShutDown()
    {
        foreach (GameObject go in _displays)
        {
            go.SetActive(false);
        }
        EyeClosed.SetActive(true);
        Noise.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
        _index = 0;
    }

    public void ClosePopUp(GameObject obj, int time, bool happy)
    {
        if (happy)
        {
            obj.GetComponentInChildren<Renderer>().material = HappyDream;
        }
        _audioSource.PlayOneShot(PopDown);
        obj.SetActive(false);
        if (_roombaFree && _ventUnblocked && _radioOn)
        {
            DisplayFinalMessage();
        }
    }

    IEnumerator OpenPopUp(GameObject obj, int time)
    {

        yield return new WaitForSeconds(time);
        _audioSource.PlayOneShot(PopUp);
        obj.SetActive(true);
        Debug.Log(obj.name);
    }

    public void DisplayFinalMessage() {
        Text.text = "I see, you helped them. Thank you. Oh... if you can do this... then maybe.... can you help... me?";
        OpenPopUp(TextBox, 3);
    }
}