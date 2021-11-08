using System.Collections.Generic;
using UnityEngine;


public class EnvironmentCube : Entity
{
    public string targetTag;
    public IEffect effect;
    protected List<GameObject> targets;

    protected void UpdateTargets()
    {
        targets.Clear();
        targets.AddRange(GameObject.FindGameObjectsWithTag(targetTag));
    }

    private bool RangePredicate(GameObject obj)
    {
        if ((obj.transform.position - this.transform.position).magnitude < effect.Range)
        {
            return true;
        }

        return false;
    }

    protected void ApplyEffect()
    {
        effect.DoOnTargets(this._rb.position, targets.FindAll(RangePredicate));
    }

    // Start is called before the first frame update
    void Start()
    {
        targets = new List<GameObject>();
        UpdateTargets();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyEffect();
    }
}
