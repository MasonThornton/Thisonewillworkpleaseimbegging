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

    public void Start()
    {
        messageText = GetComponent<TMP_Text>();
       
    }

    void Update() // Or other method
        {
       
            //grabs the score variable from the PlayerController

                messageText.SetText("score:" + MainManager.Instance.currentScore.ToString());
    
   

          

 
        }



}

