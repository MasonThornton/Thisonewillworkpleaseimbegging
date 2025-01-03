using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOVer : MonoBehaviour
{
    public PlayerController playercontroller;
    public bool respawn = false;
    public TMP_Text text;
    
    // Start is called before the first frame update
    public void Start()
    {
        text = GetComponent<TMP_Text>();
        text.color = Color.clear;
        if (respawn == true)
        {
            respawn = false;
            Debug.Log("testdebug");
        }

    }

    // Update is called once per frame
    void Update()
    {
       if (PlayerController.dead == true)
        {
            text = GetComponent<TMP_Text>();
            text.color = Color.white;
            respawn = true;

            if (Input.GetKeyDown("r"))
                {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
