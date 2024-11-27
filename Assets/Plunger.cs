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
    public bool wallHit = false;
    Vector2 boxExtents;
    // Start is called before the first frame update
    void Start()
    {

        GetComponent<BoxCollider2D>().isTrigger = false;
        boxExtents = GetComponent<BoxCollider2D>().bounds.extents;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag != "player" && collision.gameObject.tag != "plunger")


        {
            rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.gravityScale = 0.0f;


            rigidBody.velocity = new Vector2(0, 0);
       
            StartCoroutine(Sticky());
            PlayerController.plungerAmount -= 1;
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


            if (PlayerController.enablethingy == false)
            {
                GetComponent<BoxCollider2D>().isTrigger = true;

            }
            else if (PlayerController.enablethingy == true)
            {
                GetComponent<BoxCollider2D>().isTrigger = false;
            }

            if (PlayerController.plungerAmount < 2)
            {
                PlayerController.plungerAmount = 1;

            }

            if (PlayerController.plungerAmount > 2)
            {
                Destroy(gameObject);
                GetComponent<BoxCollider2D>().isTrigger = true;
                PlayerController.plungerAmount -= 1;
            }
            if (MOVE == false)
            {

                rigidBody = GetComponent<Rigidbody2D>();
                rigidBody.velocity = new Vector2(Mathf.Clamp(21f + PlayerController.pv, 21, 34) * PlayerController.plscale, 0.0f);
                MOVE = true;
            }

        }

        IEnumerator Sticky()
        {
            wallHit = true;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(0.11f);
            rigidBody.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }

