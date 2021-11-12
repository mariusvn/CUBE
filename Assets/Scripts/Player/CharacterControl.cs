using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Player))]
public class CharacterControl : MonoBehaviour
{
    public bool forceEnableControlls = false;
    public float mvtSpeed = 1.0f;
    public float gravity = -9.81f;
    public float stepHeight = 0.02f;
    public Vector3 gdCheckSize = Vector3.zero;
    public LayerMask gdCheckMask;

    private Rigidbody _rb;
    private Player _player;
    private float _startSuicide = 0.0f;
    private KeyCode _lastKey = KeyCode.None;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, gdCheckSize * 2);
    }

    bool IsGrounded()
    {
        return Physics.CheckBox(transform.position, gdCheckSize, Quaternion.Euler(0, 0, 0), gdCheckMask);
    }

    void Move()
    {
        if (forceEnableControlls || IsGrounded())
        {
            var direction = Vector3.zero;

            if (Input.GetKey(_lastKey))
            {

                if (_lastKey == KeyCode.UpArrow || _lastKey == KeyCode.W)
                    direction = Vector3.forward;
                else if (_lastKey == KeyCode.LeftArrow || _lastKey == KeyCode.A)
                    direction = Vector3.left;
                else if (_lastKey == KeyCode.DownArrow || _lastKey == KeyCode.S)
                    direction = Vector3.back;
                else if (_lastKey == KeyCode.RightArrow || _lastKey == KeyCode.D)
                    direction = Vector3.right;
            }
            else if (Input.anyKey)
            {

                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                    direction = Vector3.forward;
                else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                    direction = Vector3.left;
                else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                    direction = Vector3.back;
                else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                    direction = Vector3.right;
            }
            _rb.velocity = direction * mvtSpeed;
            if (direction != Vector3.zero)
            {
                _rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
                RaycastHit hit;
                Physics.Raycast(_rb.position + new Vector3(0, stepHeight, 0.6f), Vector3.down, out hit, stepHeight, gdCheckMask);
                if (hit.distance < stepHeight)
                {
                    _rb.velocity += new Vector3(0, 0.1f, 0);
                }
            }
        }
    }

    void Suicide()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _startSuicide = Time.time;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            _player.TrySuicide(_startSuicide);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _player.StopSuicide();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
    }

    private void OnGUI()
    {
        if (Input.anyKeyDown)
        {
            _lastKey = Event.current.keyCode;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Suicide();
    }
}
