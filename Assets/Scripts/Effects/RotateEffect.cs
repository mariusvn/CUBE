using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class RotateEffect : MonoBehaviour
{
    public string targetTag = "Player";

    public enum RotationTypes
    {
        QuarterTurn = 90,
        HalfTurn = 180,
        ThreeQuarterTurn = 270,
        CompleteTurn = 360
    }
    
    public enum RotationSide
    {
        Clockwise = 1,
        CounterClockwise = -1
    }

    public RotationTypes rotation = RotationTypes.QuarterTurn;

    public RotationSide side = RotationSide.Clockwise;

    public bool switchSideEveryUse = false;

    [Min(0f)]
    public float animationTime = 0.6f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            StartCoroutine(RotateGameObject(((float) rotation) * ((float) side), animationTime, other.transform.parent.gameObject));
        }
    }

    private void Awake()
    {
        if (side == RotationSide.Clockwise)
        {
            GameObject pane;
            try
            {
                pane = transform.Find("Arrows").gameObject;
            }
            catch (Exception)
            {
                Debug.LogWarning("No arrow child");
                return;
            }

            Vector3 scale = pane.transform.localScale;
            scale.x = -1 * scale.x;

            pane.transform.localScale = scale;
        }
    }

    private IEnumerator RotateGameObject(float angleDegrees, float duration, GameObject target)
    {
        Rigidbody ctrlr = target.gameObject.GetComponent<Rigidbody>();
        //TODO change
        var player = target.gameObject.GetComponent<CharacterControl>();
        float movementSpeed = player.mvtSpeed;

        player.mvtSpeed = 0f;
        //ctrlr.enabled = false;

        #region Rotation
        
        float step = 0.0f;
        float rate = 1.0f / duration;
        float lastStep = 0.0f;

        while (step < 1.0f)
        {
            step += Time.deltaTime * rate;
            var smoothStep = Mathf.SmoothStep(0.0f, 1.0f, step);

            Vector3 newPos = RotateAround(target.transform.position, transform.position, Vector3.up,
                angleDegrees * (smoothStep - lastStep));
            
            ctrlr.MovePosition(newPos);
            //ctrlr.MoveRotation(Quaternion.AngleAxis(angleDegrees * (smoothStep - lastStep), Vector3.up));
            
            //target.transform.RotateAround(transform.position, Vector3.up, angleDegrees * (smoothStep - lastStep));
            transform.Rotate(Vector3.up, angleDegrees * (smoothStep - lastStep));
            target.transform.Rotate(Vector3.up, angleDegrees * (smoothStep - lastStep));
            
            lastStep = smoothStep;
            yield return new WaitForFixedUpdate();
        }
        
        #endregion

        #region Translation
        
        Vector3 diff = target.transform.position - transform.position;
        diff.y = 0;
        if (diff.x > diff.z)
            diff.z = 0;
        else
            diff.x = 0;
        diff /= 4;

        duration = 0.1f;
        step = 0.0f;
        rate = 1.0f / duration;
        lastStep = 0.0f;

        while (step < 1.0f)
        {
            step += Time.deltaTime * rate;
            var smoothStep = Mathf.SmoothStep(0.0f, 1.0f, step);
            target.transform.Translate(diff * (smoothStep - lastStep), Space.World);
            lastStep = smoothStep;
            yield return null;
        }

        #endregion
        
        
        player.mvtSpeed = movementSpeed;
        if (switchSideEveryUse)
        {
            side = (RotationSide) (-1 * ((int)side));
        }
        /*
        ctrlr.enabled = true;
        player.disableMovements = false;
        */
    }
    
    private Vector3 RotateAround(Vector3 position, Vector3 rotatePoint, Vector3 axis, float angle) {
        return (Quaternion.AngleAxis(angle, axis) * (position - rotatePoint)) + rotatePoint;
    }
}
