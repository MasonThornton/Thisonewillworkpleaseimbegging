using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pole : MonoBehaviour
  
{
    Rigidbody2D rigidBody;
    float velocityconvert;
    // Start is called before the first frame update
    void Start()
    {

        
            
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(new Vector2(100000 * -PlayerController.plscale, 0.0f), ForceMode2D.Impulse);
    }

 
}
