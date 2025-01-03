using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public int Transition = 0;
    public void OnTriggerEnter2D(Collider2D collision)
    {
     
            SceneManager.LoadScene("Level"+ Transition);
            Debug.Log("hi");
        
      
    }
}
