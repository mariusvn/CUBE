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
    // [Header("Les lasers à switch on/off")]
    // public List<GameObject> switchLasers


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
            }
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
}
