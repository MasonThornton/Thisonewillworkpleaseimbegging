using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SawBlade : MonoBehaviour
{
    // backwards
    public float Saw1x;
    // forewards
    public float Saw2x;
    public float speed = 3;
    Rigidbody2D rigidBody;
    public bool state = false;
    public float factor = 5;
    // Start is called before the first frame update
    void Start()
    {
        Saw1x = transform.position.x - factor;
        Saw2x = transform.position.x + factor;
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


        if (transform.position.x <= Saw1x && state == true)
        {
      
            state = false;
            speed = -speed;
        }

        if (transform.position.x >= Saw2x && state == false)
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
                Destroy(coll.gameObject);
            }
        }
    }
}



