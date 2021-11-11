using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Material onMat;
    public Material offMat;

    [SerializeField]
    private Vector3 _at = Vector3.up / 2;
    [SerializeField]
    private Renderer _showSpawnPoint;

    public Vector3 SpawnAt { get => transform.position + _at; }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(SpawnAt, 0.5f);
        Gizmos.DrawLine(SpawnAt, SpawnAt + transform.forward);
    }

    public void UnSetSpawnPoint()
    {
        if (_showSpawnPoint != null && offMat != null)
        {
            _showSpawnPoint.material = offMat;
        }
    }

    public void SetSpawnPoint()
    {
        if (_showSpawnPoint != null && onMat != null)
        {
            _showSpawnPoint.material = onMat;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
