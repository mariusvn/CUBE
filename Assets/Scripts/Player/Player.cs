using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public TextMeshPro textId;
    public float suicideDuration = 2.0f;
    public GameObject heart;
    public bool hideId = false;
    public UnityEvent<int> onDeath;

    static int deathCount = 0;
    private SpawnPoint _spawnPoint;
    private Rigidbody _rb;
    private bool _suicided = false;
    private Vector3 _heartOrigin;
    private Renderer _heartRenderer;

    public int DeathCount {get => deathCount;}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            if (_spawnPoint)
            {
                _spawnPoint.UnSetSpawnPoint();
            }

            other.gameObject.TryGetComponent<SpawnPoint>(out _spawnPoint);
            if (_spawnPoint)
            {
                _spawnPoint.SetSpawnPoint();
            }
        }
        else if (other.CompareTag("DeathZone"))
        {
            Die();
        }
        else if (other.CompareTag("PushingUpgrade"))
        {
            UpgradesController.pushUpgrade = true;
            Destroy(other.gameObject.transform.parent.gameObject);
            _heartRenderer.material.SetColor("_EMISSION_COLOR", new Color(130f, 0f, 186f, 0f));
            ApplyUpgrades();
        }
    }

    public void Die()
    {
        onDeath.Invoke(deathCount);
        if (_spawnPoint == null)
            return;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.isKinematic = true;
        _rb.position = _spawnPoint.SpawnAt;
        ++deathCount;
        SetIDText();
        _rb.isKinematic = false;
        _rb.rotation = Quaternion.LookRotation(_spawnPoint.transform.forward, Vector3.up);
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
        {
            heart.transform.localPosition = _heartOrigin;
        }
    }

    public void StopSuicide()
    {
        heart.transform.localPosition = _heartOrigin;
        _suicided = false;
    }

    public void SetIDText()
    {
        if (textId)
        {
            textId.text = deathCount.ToString();
        }
        textId.gameObject.SetActive(!hideId);
    }

    public void ApplyUpgrades()
    {
        if (UpgradesController.pushUpgrade == true)
                    {
            foreach (GameObject cube in GameObject.FindGameObjectsWithTag("Cube"))
            {
                cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _heartRenderer = GameObject.FindGameObjectWithTag("Player Heart").GetComponent<Renderer>();
        SetIDText();
        ApplyUpgrades();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
