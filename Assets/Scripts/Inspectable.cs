using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inspectable : MonoBehaviour, IInspactable
{
    [SerializeField] Canvas canvas;
    [SerializeField] Canvas textCanvas;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] string inspectText = "";

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
    public void OnInspectStart()
    {

        textCanvas?.gameObject.SetActive(true);
        text.text = inspectText;
    }

    public void OnInspectStop()
    {
        textCanvas?.gameObject.SetActive(false);
    }
}
