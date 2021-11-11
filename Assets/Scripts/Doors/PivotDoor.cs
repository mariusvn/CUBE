using System.Collections;
using UnityEngine;

public class PivotDoor : Door
{
    public Vector3 pivot;
    public Vector3 axis = Vector3.up;
    public float angle = 90.0f;

    private Vector3 _oPos;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(pivot, 0.2f);
        Gizmos.DrawLine(pivot, pivot + axis.normalized * 1.0f);
    }

    protected override IEnumerator OpenDoor()
    {
        _animStart = Time.time;
        var tot = 0.0f;

        while (Time.time - _animStart < animDuration)
        {
            tot += angle * (Time.deltaTime / animDuration);
            door.transform.RotateAround(transform.position + pivot, axis, angle * (Time.deltaTime / animDuration));
            yield return null;
        }

        door.transform.RotateAround(transform.position + pivot, axis, angle - tot);
        inUse = false;
        isOpen = true;
        yield return null;
    }
    protected override IEnumerator CloseDoor()
    {
        _animStart = Time.time;
        var tot = angle;

        while (Time.time - _animStart < animDuration)
        {
            tot += -angle * (Time.deltaTime / animDuration);
            door.transform.RotateAround(transform.position + pivot, axis, -angle * (Time.deltaTime / animDuration));
            yield return null;
        }

        door.transform.RotateAround(transform.position + pivot, axis, - tot);
        inUse = false;
        isOpen = false;
        yield return null;
    }
    // Start is called before the first frame update
    void Start()
    {
        _oPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
