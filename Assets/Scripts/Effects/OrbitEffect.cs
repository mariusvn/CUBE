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
    [Tooltip("Must be bigger of .7 than the Sphere collider")]
    public float radiusToStop = 3f;

    [Range(0f, 360f)]
    public float anglePerSecond = 45f;

    private void Awake()
    {
        DrawRadius(radiusToStop);
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(ExecuteOrbit(other.transform.parent.gameObject));
    }

    private IEnumerator ExecuteOrbit(GameObject target)
    {
        CharacterControl ctrlr = target.GetComponent<CharacterControl>();
        Rigidbody body = target.GetComponent<Rigidbody>();
        float gravity = ctrlr.gravity;
        RigidbodyInterpolation interpolation = body.interpolation;
        RigidbodyConstraints constraints = body.constraints;

        body.useGravity = false;
        ctrlr.gravity = 0;
        ctrlr.forceEnableControlls = true;
        body.interpolation = RigidbodyInterpolation.None;
        body.constraints = RigidbodyConstraints.FreezeRotation;
        
        float dist = Vector3.Distance(transform.position, target.transform.position);
        
        while (dist < radiusToStop)
        {
            var targetPosition = body.transform.position;
            var selfPosition = transform.position;

            Vector3 newPos = RotateAbout(
                targetPosition, 
                selfPosition, 
                vectorAxises[(int)axis],
                anglePerSecond * Time.deltaTime);
            
            body.MovePosition(newPos);
            
            yield return new WaitForFixedUpdate();
            
            dist = Vector3.Distance(selfPosition, targetPosition);
        }

        ctrlr.gravity = gravity;
        body.useGravity = true;
        ctrlr.forceEnableControlls = false;
        body.interpolation = interpolation;
        body.constraints = constraints;
    }
    
    private Vector3 RotateAbout(Vector3 position, Vector3 rotatePoint, Vector3 axis, float angle) {
        return (Quaternion.AngleAxis(angle, axis) * (position - rotatePoint)) + rotatePoint;
    }

    private void DrawRadius(float radius)
    {
        const int vertexCount = 50;
        
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = vertexCount + 1;
        
        Vector3 turnAxis;
        
        switch (axis)
        {
            case AxisEnum.X:
                turnAxis = Vector3.forward;
                break;
            case AxisEnum.Y:
                turnAxis = Vector3.right;
                break;
            default: // z
                turnAxis = Vector3.up;
                break;
        }
        
        
        for (int i = 0; i < vertexCount + 1; i++)
        {
            Vector3 pos = RotateAbout(
                transform.position + (turnAxis * radius),
                transform.position, 
                vectorAxises[(int)axis],
                (360f / vertexCount) * i);
            lineRenderer.SetPosition(i, pos);
            
        }
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
