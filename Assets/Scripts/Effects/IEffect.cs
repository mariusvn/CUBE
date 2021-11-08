using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{
    double Range { get; set; }

    void DoOnTargets(Vector3 origin, List<GameObject> targets);
}
