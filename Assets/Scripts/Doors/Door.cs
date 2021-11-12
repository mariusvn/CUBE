using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float animDuration = 1.0f;
    public GameObject door;

    protected float _animStart = 0.0f;
    protected bool isOpen = false;
    protected bool inUse = false;
    public bool IsOpen { get => isOpen; }

    virtual protected IEnumerator OpenDoor()
    {
        door.SetActive(false);
        inUse = false;
        isOpen = true;
        yield return null;
    }

    virtual protected IEnumerator CloseDoor()
    {
        door.SetActive(true);
        inUse = false;
        isOpen = false;
        yield return null;
    }

    public void UseDoor()
    {
        if (inUse)
            return;
        inUse = true;
        if (isOpen)
            StartCoroutine("CloseDoor");
        else
            StartCoroutine("OpenDoor");
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
