using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Plunger : MonoBehaviour

{
    Rigidbody2D rigidBody;


    private bool MOVE = false;

    bool collided = false;


    // Start is called before the first frame update
    void Start()
    {

      
   
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag != "player" && collision.gameObject.tag != "plunger" && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))


        {
            rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.gravityScale = 0.0f;


            rigidBody.velocity = new Vector2(0, 0);

            collided = true;
            rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
      
        }

        // fix this so it stops working when player inside
        if (collision.gameObject.tag == "player" && collided == true)
        {
            gameObject.layer = 1;
        }
        else
        {
            gameObject.layer = 3;
        }









    }

    void FixedUpdate()
    {
      




        rigidBody = GetComponent<Rigidbody2D>();
      
            if (PlayerController.plungerRotation < 0)
            {
                rigidBody.rotation = 180;
            }
            else
            {
                rigidBody.rotation = 0;
            }
  

      

   
       

     

        if (PlayerController.shooting == true && collided == true)
        {
            PlayerController.fire = true;
           Destroy(gameObject);

           
        }
     
         
        if (MOVE == false)
            {

                rigidBody = GetComponent<Rigidbody2D>();
                rigidBody.velocity = new Vector2(Mathf.Clamp(43f + PlayerController.pv, 43, 63) * PlayerController.plscale, 0.0f);
                MOVE = true;
            }

        }

    


}

