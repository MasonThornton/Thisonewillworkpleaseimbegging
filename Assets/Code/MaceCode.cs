using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class MaceCode : MonoBehaviour
{


   Rigidbody2D rigidBody;
    // identity of the object 
    // time until falling 
    float waitTimer = 0.2f;
    // start
    bool beginWait = false;
 
    public float startingy;
  
    // variable for telling the thing to start going back
    public bool returnTime = false;
    // timer until it needs to return
    public float returnTimer = 1;
    public float disbet;
    public bool inside = false;


    // Start is called before the first frame update

    // used by the plunger to know the position of the mace

    void Start()
    {

        rigidBody = GetComponent<Rigidbody2D>();
        {
 
            startingy = transform.position.y;
       

        }

    }




    // Update is called once per frame
    void Update()
    {

  
        rigidBody = GetComponent<Rigidbody2D>();
        disbet = transform.position.y - startingy;

     
        if (beginWait == true)
        {
            rigidBody.bodyType = RigidbodyType2D.Dynamic;
            rigidBody.gravityScale = 1;
          





        }

        

        //code that moves the mace back up after it hit the floor
        if (returnTime == true)
        {
            rigidBody.velocity = new Vector2(0, Mathf.Clamp(-disbet, 1, 3));
            rigidBody.gravityScale = 0;
        
            beginWait = false;
        }

        //after you are done reset everything
        if (transform.position.y >= startingy && returnTime == true)
        {
            returnTime = false;
            beginWait = false;
            rigidBody.velocity = new Vector2(0, 0);
            rigidBody.bodyType = RigidbodyType2D.Static;
  //if the player continues to be inside then begin to fall immedietly
            if (inside == true)
            {
                StartCoroutine(startToFall());
            }
            


   


        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {



        //begins to fall if player is inside the collision box

        if (coll.gameObject.tag == "Player")

        {
            StartCoroutine(startToFall());
 
            inside = true;
        }



    }

    //kills the player
    public void OnCollisionEnter2D(Collision2D collision)
    {
   
        if (collision.gameObject.tag == "Player")
     
          
            {
                inside = false;
            PlayerController.dead = true;
    
        }
   
            


        // if it hits the floor then it pauses then starts to go back up

        if (collision.gameObject.tag == "ground")
        {

            {


                if (transform.position.y != startingy)
                {
                   
                    StartCoroutine(waitTillReturn());
                    MainManager.Instance.ThudSound();


                }
            }
        }
    }
    private IEnumerator waitTillReturn()
    {
        yield return new WaitForSeconds(returnTimer);
      
            returnTime = true;

     
}

    private IEnumerator startToFall()
    {
        yield return new WaitForSeconds(waitTimer);
        if (inside == true)
        {
            beginWait = true;
        }
   


    }



    void OnTriggerExit2D(Collider2D coll)
        {

            if (coll.gameObject.tag == "Player")

                {
         
            inside = false;
                  
                }


            


            }
        }
   



