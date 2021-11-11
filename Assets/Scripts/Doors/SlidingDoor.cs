using System.Collections;
using UnityEngine;

public class SlidingDoor : Door
{
    public Vector3 direction;
    public float distance;

    private Vector3 _origin;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(door.transform.localPosition, door.transform.localPosition + direction.normalized * distance);
    }

    protected override IEnumerator OpenDoor()
    {
        _animStart = Time.time;
        var to = door.transform.localPosition + direction.normalized * distance;

        while (Time.time - _animStart < animDuration)
        {
            door.transform.localPosition = Vector3.Lerp(_origin, to, (Time.time - _animStart) / animDuration);
            yield return null;
        }

        door.transform.localPosition = to;
        inUse = false;
        isOpen = true;
        yield return null;
    }
    protected override IEnumerator CloseDoor()
    {
        _animStart = Time.time;
        var from = door.transform.localPosition;

        while (Time.time - _animStart < animDuration)
        {
            door.transform.localPosition = Vector3.Lerp(from, _origin, (Time.time - _animStart) / animDuration);
            yield return null;
        }

        door.transform.localPosition = _origin;

        inUse = false;
        isOpen = false;
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        _origin = door.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
