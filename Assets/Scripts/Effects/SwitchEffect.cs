using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchEffect : MonoBehaviour
{
    [HideInInspector]
    public bool isActive = false;
    public string targetTag = "Player";
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            isActive = !isActive;
        }
    }
}
