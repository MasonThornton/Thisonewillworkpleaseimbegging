using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class SawCode : MonoBehaviour
{
    Rigidbody2D rigidBody;
    // identity of the object 
    // time until falling 
    float waitTimer = 2;
    // start
    float beginWait = 0;
    public float startingx;
    public float startingy;
    // current
    // variable for telling the thing to start going back
    public bool returntime = false;
    // timer until it needs to return
    public float returntimer = 2;
    public float disbet;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        {
            startingx = transform.position.x;
            startingy = transform.position.y;

        }

    }




    // Update is called once per frame
    void Update()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        disbet = transform.position.y - startingy;

        if (beginWait == 1)
        {
            waitTimer -= Time.deltaTime;
            Debug.Log("2");
        }
        if (waitTimer < 0)
        {

            rigidBody.gravityScale = 1;
            beginWait = 0;
            waitTimer = 15;
            returntime = true;

            Debug.Log("1");
        }

        if (returntime == true)
        {
            returntimer -= Time.deltaTime;
            Debug.Log("AHey");
        }

        if (returntimer < 0)
        {
            returntime = false;
            Debug.Log("disbet");
        }


        if (returntimer < 0)
        {
            rigidBody.velocity = new Vector2(0, Mathf.Clamp(-disbet,1,3));
            rigidBody.gravityScale = 0;
            Debug.Log(disbet);
        }

        if (transform.position.y >= startingy && returntimer < 0)
        {
            returntime = false;
            rigidBody.velocity = new Vector2(0,0);
            returntimer = 2;
            waitTimer = 2;


        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {





        if (coll.gameObject.tag == "Player")

        {
            beginWait = 1;
            Debug.Log("1");
        }



    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
   
        if (collision.gameObject.tag == "Player")
        {

            {
                Destroy(collision.gameObject);
            }
        }
            




        if (collision.gameObject.tag == "ground")
        {

            {


                if (transform.position.y != startingy)
                {
                    returntime = true;

                }
            }
        }
    }



    void OnTriggerExit2D(Collider2D coll)
        {

            if (coll.gameObject.tag == "Player")

                {
                    waitTimer = 2;
                    beginWait = 0;
                }


            


            }
        }
   



