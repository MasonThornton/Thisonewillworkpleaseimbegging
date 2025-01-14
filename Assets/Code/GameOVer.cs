using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOVer : MonoBehaviour
{
    public PlayerController playercontroller;

    public TMP_Text text;

    public AudioSource GameOver;

    public bool PlaySound = true;
    // Start is called before the first frame update
    public void Start()
    {
        text = GetComponent<TMP_Text>();
        text.color = Color.clear;


    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.dead == true)
        {
            text = GetComponent<TMP_Text>();
            text.color = Color.white;
            GameOver = GetComponent<AudioSource>();
             
            if (PlaySound == true)
            {

                GameOver.Play();
                PlaySound = false;

            }

            if (Input.GetKeyDown("r"))
            {

                MainManager.Instance.currentScore = MainManager.Instance.Score;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                PlaySound = true;
            }
        }


    }
}
