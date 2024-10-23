using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
<<<<<<< HEAD
using Unity.VisualScripting;
using System;

public class TextUpdater : MonoBehaviour
{
    public static float score2 = PlayerController.score;
=======

public class TextUpdater : MonoBehaviour
{
>>>>>>> 1107c0aaac5b617547ee5a4df820136f0fec662b
    public TMP_Text messageText;

    void Update() // Or other method
    {
        if (gameObject.tag == "text")
            {
<<<<<<< HEAD
            messageText.SetText(score2.ToString());
   
=======
            messageText.SetText("New message");
>>>>>>> 1107c0aaac5b617547ee5a4df820136f0fec662b
        }
    }
}