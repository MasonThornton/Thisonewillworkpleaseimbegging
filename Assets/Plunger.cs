using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
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
            gameObject.layer = 6;
        }
        else if (collision.gameObject.tag != "player")
        {
            gameObject.layer = 3;
        }

        








    }

    void FixedUpdate()
    {
      




        rigidBody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        if (PlayerController.plungerRotation < 0 )
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






        if (collided == true)
        {
            Vector2 hitBoxSize = new Vector2(boxExtents.x * 2.0f, 0.05f);

            Vector2 edge = new Vector2(transform.position.x + -PlayerController.plungerRotation, transform.position.y);

            RaycastHit2D result = Physics2D.BoxCast(edge, hitBoxSize, 0.0f, new Vector3(1222222222222222.0f * -PlayerController.plungerRotation, 0.0f), 0.0f, 1 << LayerMask.NameToLayer("Ground"));

            bool wall = result.collider != null && result.normal.x > 0.9f;

            if (wall == false)
            {
                PlayerController.fire = true;
                Debug.Log("hi")

            }
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

