using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEffect : MonoBehaviour
{
    public Vector3 destination = Vector3.forward;
    public string targetTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            try
            {
                other.gameObject.GetComponent<CharacterController>().Move(destination);
            }
            catch (Exception e)
            {
                Debug.LogError("Target player game object doesn't have any CharacterController");
            }
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Vector3 position = transform.position;
        Gizmos.color = new Color(0, 0, 1, 0.5f);;
        Gizmos.DrawLine(position, position + destination);
        Gizmos.DrawCube(position + destination, Vector3.one);
    }

#endif
}
