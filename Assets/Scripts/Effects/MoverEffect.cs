using System.Collections;
using UnityEngine;

public class MoverEffect : MonoBehaviour
{
    public Vector3 destination = Vector3.forward;
    public string targetTag = "Player";
    [Min(0f)]
    public float duration = .3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            StartCoroutine(Translate(duration, transform.position + destination, other.gameObject));
        }
    }

    private IEnumerator Translate(float duration, Vector3 dest, GameObject target)
    {
        CharacterController ctrlr = target.gameObject.GetComponent<CharacterController>();
        //TODO change
        var player = target.gameObject.GetComponent<tmpPlayer>();

        Vector3 diff = dest - target.transform.position;
        Debug.Log(diff);
        
        player.disableMovements = true;
        ctrlr.enabled = false;

        #region Animation
        
        float step = 0.0f;
        float rate = 1.0f / duration;
        float lastStep = 0.0f;

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

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Vector3 position = transform.position;
        Gizmos.color = new Color(0, 0, 1, 0.5f);;
        Gizmos.DrawLine(position, position + destination);
        Gizmos.DrawCube(position + destination, Vector3.one);
    }

#endif
}
