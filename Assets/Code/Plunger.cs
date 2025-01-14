using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Plunger : MonoBehaviour

{
    Rigidbody2D rigidBody;

    SpriteRenderer SpriteRenderer;
    private bool MOVE = false;
    Vector2 boxExtents;
    bool collided = false;
    public bool FollowMoveable = false;
    public GameObject Mace = null;
    public bool Maced = false;
    public float currenty;


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
            gameObject.layer = 6;
        }
        else if (collision.gameObject.tag != "player")
        {
            gameObject.layer = 3;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Mace"))
        {
            PlayerController.fire = true;
            MainManager.Instance.BreakSound();
            Destroy(gameObject);


        }











    }

    void FixedUpdate()
    {
        rigidBody = GetComponent<Rigidbody2D>();
      



 
        SpriteRenderer = GetComponent<SpriteRenderer>();

        if (PlayerController.plungerRotation < 0)
        {
            rigidBody.rotation = 180;
            // makes it visible after rotating it
            SpriteRenderer.color = Color.white;
        }
        else
        {
            rigidBody.rotation = 0;
            // makes it visible after rotating it
            SpriteRenderer.color = Color.white;
        }



        if (PlayerController.PlungerRemove == true || PlayerController.shooting == true && collided == true)
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

