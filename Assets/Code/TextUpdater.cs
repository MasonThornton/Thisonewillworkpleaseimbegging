using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

using Unity.VisualScripting;
using System;
using UnityEngine.SocialPlatforms.Impl;

public class TextUpdater : MonoBehaviour
{



     TMP_Text messageText;
    public float score;
    public float maxscore;

    public void Start()
    {
        messageText = GetComponent<TMP_Text>();
        if (PlayerController.dead == true)
        {
            score = maxscore;
        }
        else { maxscore = score; }
     

    }

    void Update() // Or other method
        {
       
            //grabs the score variable from the PlayerController

                messageText.SetText("score:" + score.ToString());
    
   

          

 
        }



}

