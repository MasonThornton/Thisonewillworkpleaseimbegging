using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Plunger : MonoBehaviour

{
    Rigidbody2D rigidBody;
    float velocityconvert;
    float plungertimer;
    private bool MOVE = false;
    public bool wallHit = false;
    float directionplunger;
    bool collided = false;

    Vector2 boxExtents;
    // Start is called before the first frame update
    void Start()
    {

        GetComponent<BoxCollider2D>().isTrigger = true;
        boxExtents = GetComponent<BoxCollider2D>().bounds.extents;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag != "player" && collision.gameObject.tag != "plunger" && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))


        {
            rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.gravityScale = 0.0f;


            rigidBody.velocity = new Vector2(0, 0);
       
            StartCoroutine(Sticky());
     

        }







    }

    void FixedUpdate()
    {

     

 
        rigidBody = GetComponent<Rigidbody2D>();
        if (wallHit == false)
        {
            if (PlayerController.plscale == -1)
            {
                rigidBody.rotation = 180;
            }
            else
            {
                rigidBody.rotation = 0;
            }
        }

      
            directionplunger = PlayerController.plscale;
   
       

     

        if (PlayerController.shooting == true && collided == true)
        {
            PlayerController.fire = true;
            GameObject.Destroy(gameObject);

            StartCoroutine(Die());
        }

        if (PlayerController.enablethingy == false)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;

        }
        else if (PlayerController.enablethingy == true)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
         
        if (MOVE == false)
            {

                rigidBody = GetComponent<Rigidbody2D>();
                rigidBody.velocity = new Vector2(Mathf.Clamp(43f + PlayerController.pv, 43, 63) * PlayerController.plscale, 0.0f);
                MOVE = true;
            }

        }

        IEnumerator Sticky()
        {
            wallHit = true;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(0.11f);
            rigidBody.constraints = RigidbodyConstraints2D.FreezePosition;
        collided = true;
        }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.01f);
   

    }
}

