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

    [Min(0f)]
    public float animationTime = 0.6f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            StartCoroutine(RotateGameObject(((float) rotation) * ((float) side), animationTime, other.gameObject));
        }
    }

    private IEnumerator RotateGameObject(float angleDegrees, float duration, GameObject target)
    {
        CharacterController ctrlr = target.gameObject.GetComponent<CharacterController>();
        //TODO change
        var player = target.gameObject.GetComponent<tmpPlayer>();
        
        player.disableMovements = true;
        ctrlr.enabled = false;

        #region Rotation
        
        float step = 0.0f;
        float rate = 1.0f / duration;
        float lastStep = 0.0f;

        while (step < 1.0f)
        {
            step += Time.deltaTime * rate;
            var smoothStep = Mathf.SmoothStep(0.0f, 1.0f, step);
            target.transform.RotateAround(transform.position, Vector3.up, angleDegrees * (smoothStep - lastStep));
            transform.Rotate(Vector3.up, angleDegrees * (smoothStep - lastStep));
            lastStep = smoothStep;
            yield return null;
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
        
        ctrlr.enabled = true;
        player.disableMovements = false;
    }
}
