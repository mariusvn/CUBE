using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public int playerId;
    public TextMeshPro textId;
    public float suicideDuration = 2.0f;
    public GameObject heart;
    public GameObject deadBody;

    private Transform _spawnPoint;
    private Rigidbody _rb;
    private bool _suicided = false;
    private Vector3 _heartOrigin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            _spawnPoint = other.gameObject.transform;
        }
        else if (other.CompareTag("DeathZone"))
        {
            Die();
        }
    }

    public void Die()
    {
        _rb.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        _rb.position = _spawnPoint.position;
        if (deadBody != null)
        {
            var obj = Instantiate<GameObject>(deadBody.gameObject, new Vector3(0.0f, 10.0f, 0.0f), Random.rotation);
            obj.GetComponentInChildren<TextMeshPro>().text = playerId.ToString();
        }
        ++playerId;
        SetIDText();
    }

    public void TrySuicide(float startTime)
    {
        if (_suicided == false)
        {
            heart.transform.localPosition = _heartOrigin + new Vector3(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f) / 4;
            if (Time.time - startTime > suicideDuration)
            {
                Die();
                _suicided = true;
            }
        }
        else
            heart.transform.localPosition = _heartOrigin;
    }

    public void StopSuicide()
    {
        heart.transform.localPosition = _heartOrigin;
        _suicided = false;
    }

    void SetIDText()
    {
        if (textId)
        {
            textId.text = playerId.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        SetIDText();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
