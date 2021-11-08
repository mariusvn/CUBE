using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Entity : MonoBehaviour
{

    public ParticleSystem deathParicles;
    protected Rigidbody _rb;

    public void Kill()
    {
        if (deathParicles)
        {
            deathParicles.Play();
        }
        else if (!deathParicles || deathParicles.IsAlive())
        {
            GameObject.Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
