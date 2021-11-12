using UnityEngine;

public class ButtonEffect : MonoBehaviour
{
    public bool IsActive => _goInside > 0;
    public string targetTag = "Player";
    public GameObject buttonObject;

    private int _goInside = 0;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    private const float ButtonPressedOffset = 0.08f;

    private void OnTriggerEnter(Collider other)
    {
        print("Test");
        if (other.CompareTag(targetTag))
        {
            _goInside++;
            if (IsActive && buttonObject)
            {
                buttonObject.GetComponent<MeshRenderer>().material.SetVector(EmissionColor, new Color(0.867f, 0.062f, 0.062f) * 0.7f);
                Vector3 buttonPos = buttonObject.transform.position;
                buttonPos.y -= ButtonPressedOffset;
                buttonObject.transform.position = buttonPos;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            _goInside--;
            if (!IsActive && buttonObject)
            {
                buttonObject.GetComponent<MeshRenderer>().material.SetVector(EmissionColor, Color.black);
                Vector3 buttonPos = buttonObject.transform.position;
                buttonPos.y += ButtonPressedOffset;
                buttonObject.transform.position = buttonPos;
            }
        }
    }
}
