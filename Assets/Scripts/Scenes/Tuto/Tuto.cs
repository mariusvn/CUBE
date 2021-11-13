using TMPro;
using UnityEngine;

public class Tuto : MonoBehaviour
{
    public GameObject player;
    public GameObject deadBody;
    public Door lvlEnd;
    public Bridge bridge;

    public void OnPlayerDeath(int count)
    {
        if (deadBody != null)
        {
            var obj = Instantiate<GameObject>(deadBody.gameObject, new Vector3(0.0f, 10.0f, 0.0f), Random.rotation);
            obj.GetComponentInChildren<TextMeshPro>().text = count.ToString();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        lvlEnd.UseDoor();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
