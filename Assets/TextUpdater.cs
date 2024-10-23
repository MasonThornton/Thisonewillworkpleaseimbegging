using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

using Unity.VisualScripting;
using System;

public class TextUpdater : MonoBehaviour
{
    public static float score2 = PlayerController.score;


    public TMP_Text messageText;

    void Start()
    {


    }

    void Update() // Or other method
        {
            if (gameObject.tag == "text")
            {
            score2 = PlayerController.score;
                messageText.SetText(score2.ToString());


          

            }
        }
    }

