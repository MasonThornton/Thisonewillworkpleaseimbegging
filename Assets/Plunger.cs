using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Plunger : MonoBehaviour

{
    Rigidbody2D rigidBody;
    float velocityconvert;
    float plungertimer;
    private bool MOVE = false;
    float timer = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        rigidBody.isKinematic = false;


    }
    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "door")


        {
            rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.gravityScale = 0.0f;
           
            rigidBody.velocity = new Vector2(0, 0);
            rigidBody.rotation = 0;
            rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

        }






    }

    void Update()
    {
        if (PlayerController.enablethingy == false)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
          
        }
        else if (PlayerController.enablethingy == true)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }


        if (PlayerController.plungerAmount > 1)
        {
            Destroy(gameObject);
            PlayerController.plungerAmount = 1;
        }
        if (MOVE == false)
        {

            rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.AddForce(new Vector2(Mathf.Clamp(21f + PlayerController.pv,21,34) * PlayerController.plscale, 0.0f), ForceMode2D.Impulse);
            MOVE = true;
        }
    }

 

}


