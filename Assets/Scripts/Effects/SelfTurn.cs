using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfTurn : MonoBehaviour
{
    public float degreesPerSecond = 50f;
    public AnimationCurve curve;
    public float curveTime = 10f;
    void Update()
    {
        transform.Rotate(new Vector3(0, degreesPerSecond * Time.deltaTime, 0));
        float scale = curve.Evaluate((Time.time % curveTime) / curveTime);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
