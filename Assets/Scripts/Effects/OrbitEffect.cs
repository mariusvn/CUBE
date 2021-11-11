using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitEffect : MonoBehaviour
{
    public enum AxisEnum
    {
        X = 0,
        Y = 1,
        Z = 2
    };

    private Vector3[] vectorAxises = {
        Vector3.right, Vector3.up, Vector3.forward
    };

    public AxisEnum axis = AxisEnum.Y;
    
    [Min(1f)]
    [Tooltip("Must be bigger than the Sphere collider")]
    public float radiusToStop = 3f;

    [Range(0f, 360f)]
    public float anglePerSecond = 45f;
    
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(ExecuteOrbit(other.transform.parent.gameObject));
    }

    private IEnumerator ExecuteOrbit(GameObject target)
    {
        CharacterControl ctrlr = target.GetComponent<CharacterControl>();
        Rigidbody body = target.GetComponent<Rigidbody>();
        float gravity = ctrlr.gravity;

        body.useGravity = false;
        ctrlr.gravity = 0;
        
        float dist = Vector3.Distance(transform.position, target.transform.position);
        
        while (dist < radiusToStop)
        {
            var targetPosition = target.transform.position;
            var selfPosition = transform.position;

            Vector3 newPos = RotateAbout(
                targetPosition, 
                selfPosition, 
                vectorAxises[(int)axis],
                anglePerSecond * Time.deltaTime);
            //Vector3 diff = targetPosition - newPos;
            
            body.MovePosition(newPos);
            
            yield return null;
            
            dist = Vector3.Distance(selfPosition, targetPosition);
        }

        ctrlr.gravity = gravity;
        body.useGravity = true;
    }
    
    private Vector3 RotateAbout(Vector3 position, Vector3 rotatePoint, Vector3 axis, float angle) {
        return (Quaternion.AngleAxis(angle, axis) * (position - rotatePoint)) + rotatePoint;
    }


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Vector3 position = transform.position;
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawSphere(position, .2f);
        Gizmos.color = new Color(0.7f, 0, 0.2f, 0.5f);
        Gizmos.DrawWireSphere(position, radiusToStop);
    }

#endif
}
