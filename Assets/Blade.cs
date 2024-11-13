using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SawBlade : MonoBehaviour
{


    public float speed = 3;
    Rigidbody2D rigidBody;
    public bool state = false;
    public float factor = 5;
    // the position of the left and right of the x
    float[] sawx = { 0, 1 };
    // Start is called before the first frame update
    void Start()
    {

        sawx[0] = transform.position.x - factor;
        sawx[1] = transform.position.x + factor;
            rigidBody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.rotation += -speed;
       
        if (state == false)
        {
            rigidBody.velocity = new Vector2(speed, 0);
        }

        if (state == true)
        {
            rigidBody.velocity = new Vector2(speed, 0);
        }


        if (transform.position.x <= sawx[0] && state == true)
        {
      
            state = false;
            speed = -speed;
            
        }

        if (transform.position.x >= sawx[1] && state == false)
        {
  
            state = true;
            speed = -speed;
        }

    }

    public void OnTriggerEnter2D(Collider2D coll)     
    {

        if (coll.gameObject.tag == "Player")
        {

            {
                PlayerController.dead = true;
            }
        }
    }
}



