using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateEffect : MonoBehaviour
{
    public AnimationCurve curve;
    public float curveTime = 2f;
    
    public enum AxisEnum
    {
        X = 0,
        Y = 1,
        Z = 2
    };

    public AxisEnum axis = AxisEnum.X;

    private Vector3[] vectorAxises = {
        Vector3.right, Vector3.up, Vector3.forward
    };

    private Vector3 originalPos;

    private void Awake()
    {
        originalPos = transform.position;
    }

    void Update()
    {
        float translate = curve.Evaluate((Time.time % curveTime) / curveTime);
        Vector3 pos = originalPos + translate * vectorAxises[(int)axis];
        transform.position = pos;
    }
}
