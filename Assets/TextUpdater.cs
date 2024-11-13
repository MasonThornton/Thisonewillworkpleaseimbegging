using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

using Unity.VisualScripting;
using System;

public class TextUpdater : MonoBehaviour
{



    public TMP_Text messageText;

    void Start()
    {


    }

    void Update() // Or other method
        {
            if (gameObject.tag == "text")
            {
            //grabs the score variable from the PlayerController

                messageText.SetText(PlayerController.score.ToString());


          

            }
        }
    }

