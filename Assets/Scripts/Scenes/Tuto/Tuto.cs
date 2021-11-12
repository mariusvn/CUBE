using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tuto : MonoBehaviour
{
    public GameObject deadBody;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
