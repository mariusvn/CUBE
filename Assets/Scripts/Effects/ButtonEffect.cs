using UnityEngine;

public class ButtonEffect : MonoBehaviour
{
    public bool IsActive => _goInside > 0;
    public string targetTag = "Player";

    private int _goInside = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            _goInside++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            _goInside--;
        }
    }
}
