using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class SawCode : MonoBehaviour
{
    Rigidbody2D rigidBody;
    // identity of the object 
    float me = 0;
    // time until falling 
    float waitTimer = 5;
    // start
    float beginWait = 0;
    public float startingx;
    public float startingy;
    // current
    public float currentx;
    public float currenty;
    // variable for telling the thing to start going back
    public bool returntime = false;
    // timer until it needs to return
    public float returntimer = 5;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        if (gameObject.tag == "mace")
        {
            startingx = transform.position.x;
            startingy = transform.position.y;
            me = 1;
        }

        if (gameObject.tag == "mace2")
        {

            me = 2;
        }
    }




    // Update is called once per frame
    void Update()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        currenty = transform.position.y;
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
            Debug.Log("Hey");

            if (gameObject.tag == "mace2" && returntimer < 0)
            {
                transform.position += new Vector3(1, 1);
                rigidBody.gravityScale = 0;
                Debug.Log("Hey2");
            }
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Player")
        {
            if (me == 1)
            {
                Destroy(coll.gameObject);
            }
        }



        if (coll.gameObject.tag == "Player")
        {
            if (me == 2)
            {
                beginWait = 1;
                Debug.Log("1");
            }
        }

        if (coll.gameObject.tag == "ground")
        {
            if (me == 1)
            {


                if (currenty != startingy)
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
                if (me == 2)
                {
                    waitTimer = 5;
                    beginWait = 0;
                }


            


            }
        }
    }



