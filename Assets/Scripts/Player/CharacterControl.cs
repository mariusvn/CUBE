using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Player))]
public class CharacterControl : MonoBehaviour
{
    public float mvtSpeed = 1.0f;
    public float gravity = -9.81f;
    public Vector3 gdCheckSize = Vector3.zero;
    public LayerMask gdCheckMask;

    private Rigidbody _rb;
    private Player _player;
    private Vector3 _velocity = Vector3.zero;
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
        if (IsGrounded())
        {
            var direction = Vector3.zero;
            RaycastHit hit;

            _velocity = Vector3.zero;

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


            Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 2.0f, gdCheckMask);

            _rb.position += Vector3.up * (hit.point.y - _rb.position.y);
            if (direction != Vector3.zero)
            {
                _rb.position += direction * mvtSpeed * Time.deltaTime;
                _rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
            }
        }
        else
        {
            _velocity += new Vector3(0.0f, 0.5f * _rb.mass * gravity * Time.deltaTime, 0.0f);
            _rb.position += _velocity * Time.deltaTime;
        }
    }

    void Suicide()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            _startSuicide = Time.time;
        }
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            _player.TrySuicide(_startSuicide);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
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
