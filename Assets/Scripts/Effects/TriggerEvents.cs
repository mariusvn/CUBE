using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvents : MonoBehaviour
{
    public bool triggerOnce;

    [Header("Les lasers à switch on/off")]
    public List<GameObject> switchLasers;
    [Header("Les objets avec un script de spin à stopé/lancé")]
    public List<GameObject> switchSpinners;
    [Header("Les portes à ouvrir")]
    public List<GameObject> doors;
    [Header("L'objet à monté")]
    public GameObject raisedObject;
    public float height = 0;
    // public GameObject raisedObject2;
    // public float height2 = 0;
    //public float rasingSpeed = 1;


    private int trigger = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<ButtonEffect>().IsActive == true) {
            if (triggerOnce && trigger == 0) {
                trigger = 1;
                SwitchLasers();
                SwitchSpinners();
                OpenDoors();
                RaiseDeathZone();
            } else if (!triggerOnce && trigger == 0) {
                trigger = 1;
                SwitchLasers();
                SwitchSpinners();
                OpenDoors();
            }
        }
        else {
            if (!triggerOnce && trigger == 1)
                trigger = 0;
        }
    }

    void SwitchLasers()
    {
        foreach (GameObject laser in switchLasers) {
            if (laser.GetComponent<Laser>().activated == true) {
                laser.GetComponent<Laser>().Deactivate();
            } else if (laser.GetComponent<Laser>().activated == false) {
                laser.GetComponent<Laser>().Activate();
            }
        }
    }

    void SwitchSpinners()
    {
        foreach (GameObject spinner in switchSpinners) {
            if (spinner.GetComponent<SpinEffect>().spinSpeed >= 1f) {
                spinner.GetComponent<SpinEffect>().spinSpeed /= 1000f;
            } else if (spinner.GetComponent<SpinEffect>().spinSpeed < 1f) {
                spinner.GetComponent<SpinEffect>().spinSpeed *= 1000f;
            }
        }
    }

    void OpenDoors()
    {
        foreach (GameObject door in doors) {
            if (door.GetComponent<Door>().IsOpen == true) {
                door.GetComponent<Door>().UseDoor();
            } else if (door.GetComponent<Door>().IsOpen == false) {
                door.GetComponent<Door>().UseDoor();
            }
        }
    }

    void RaiseDeathZone()
    {
        if (raisedObject && height != 0)
            raisedObject.transform.position =  new Vector3(raisedObject.transform.position.x, height, raisedObject.transform.position.z);
        // if (raisedObject2 && height2 != 0)
        //     raisedObject2.transform.position =  new Vector3(transform.position.x, height, transform.position.z);
    }
}
