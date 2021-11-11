using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraControl : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float distanceFromTarget;

    void FollowTarget()
    {
        if (target)
        {
            transform.position = target.position + offset * distanceFromTarget;
            transform.LookAt(target);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }
}
