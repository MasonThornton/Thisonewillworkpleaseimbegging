using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public int Transition = 0;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        MainManager.Instance.Score = MainManager.Instance.currentScore;
            SceneManager.LoadScene("Level"+ Transition);
       
    
      
    }
}
