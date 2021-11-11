using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject mainMenu;
    
    public void OnPlayerDeath()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mainMenu.SetActive(true);
        }
        if (mainMenu.activeSelf)
            Time.timeScale = 0.0f;
    }
}
