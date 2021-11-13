using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchEffect : MonoBehaviour
{
    [HideInInspector]
    public bool isActive = false;
    public string targetTag = "Player";
    public GameObject buttonObject;
    public GameObject indicatorObject;
    
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    private const float ButtonPressedOffset = 0.08f;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            isActive = !isActive;
            if (buttonObject)
            {
                Vector3 buttonPos = buttonObject.transform.position;
                buttonPos.y -= ButtonPressedOffset;
                buttonObject.transform.position = buttonPos;
            }
            if (indicatorObject)
            {
                if (isActive)
                    indicatorObject.GetComponent<MeshRenderer>().material.SetVector(EmissionColor, new Color(0.867f, 0.062f, 0.062f) * 0.7f);
                else
                    indicatorObject.GetComponent<MeshRenderer>().material.SetVector(EmissionColor, Color.black);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (buttonObject)
        {
            Vector3 buttonPos = buttonObject.transform.position;
            buttonPos.y += ButtonPressedOffset;
            buttonObject.transform.position = buttonPos;
        }
    }
}
