using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using System.ComponentModel;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    public AudioSource coinSound;
    Rigidbody2D rigidBody;
    float speed = 7.0f;
    public float jumpForce = 8.0f;
    public float dashForce = 100000.0f;
    public static float pv;
    public bool isDashing = false;
    public double dashTimer = 0.1;

    public float dashDist = 25;
    public float dashGreatLessChecked = 0;
    public float dashDif;
    public float airControlForce = 10.0f;
    public float airControlMax = 3.5f;
    public static bool dead = false;
    public static float score = 0;
    public static float py;
    public static float plscale = -1;
    public static List<float> PlayerInventory = new List<float>();
    public bool running = false;
    public GameObject Plunger;
    public static float plungerAmount = 0;



    Vector2 boxExtents;
    public TMP_Text messageText;

    //use this for initialization
    void Start()
    {
        plscale = -transform.localScale.x;
        rigidBody = GetComponent<Rigidbody2D>();
        boxExtents = GetComponent<BoxCollider2D>().bounds.extents;
        animator = GetComponent<Animator>();

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "coin")
        {
            Destroy(coll.gameObject);
            score += 100;
            Debug.Log(score);
            coinSound.Play();

        }

        if (coll.gameObject.tag == "key")
        {


            Destroy(coll.gameObject);



        }
    }


    //Update is called once per frame
    void Update()

    {
        Vector3 pv = rigidBody.velocity;
        py = transform.position.y;

        if (gameObject.tag == "Player" && dead == true)


        {

            Destroy(gameObject);
        }
        {

        }
        float blinkVal = Random.Range(0.0f, 900.0f);
        if (blinkVal < 1.0f)
            animator.SetTrigger("blinktrigger");

        

        if (rigidBody.velocity.x * transform.localScale.x < 0.0f)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        float xSpeed = Mathf.Abs(rigidBody.velocity.x);
        animator.SetFloat("xspeed", xSpeed);
        float ySpeed = Mathf.Abs(rigidBody.velocity.y);
        animator.SetFloat("yspeed", ySpeed);

        float h = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown("f") && h != 0 && SpawnCollider.inside == false)
        {
            plungerAmount += 1;
    Instantiate(Plunger, new Vector2(transform.position.x + 1f * plscale ,transform.position.y), Quaternion.identity);


        }
    }






    void FixedUpdate()
    {
        dashDif = transform.position.x - dashDist;

        // a timer used to make a check but not instantly so it doesnt insta cancel my dash
        if (isDashing == true)
        {
            dashTimer -= Time.deltaTime;
        }



            // sets h to the value of my inputs a/d a is -1 and d is 1

        float h = Input.GetAxis("Horizontal");

        // if dashing is == true and my position is less than dash dist set this value to 1 otherwise 2 
        if (isDashing == true && transform.position.x < dashDist)
        {
            dashGreatLessChecked = 1;
        }

        else if (isDashing == true && transform.position.x > dashDist)
            {

            dashGreatLessChecked = 2;
        }
        // used for positional movement used for my dash code and other things
        if (h > 0 && isDashing == false || h < 0 && isDashing == false )
        {
            plscale = h;
        }
        // super complex code that checks if im dashing, whether or not it needs to check for greater or less and if im close to my approximate destination

        if (dashDif > 1.1 && dashGreatLessChecked == 1 && isDashing == true || dashDif < 1.1 && isDashing == true && dashGreatLessChecked == 2 || isDashing == true && dashTimer <= 0 && pv == 0)
        {
            isDashing = false;
            running = true;
            rigidBody.velocity = new Vector2(0,0);
            dashTimer = 0.1;
        }


     

     



        /*if (h != 0.0f)
        {
            rigidBody.velocity = new Vector2(h * speed, 0.0f);
        }*/

        //Check if we are on the ground
        Vector2 bottom = new Vector2(transform.position.x, transform.position.y - boxExtents.y);

        Vector2 hitBoxSize = new Vector2(boxExtents.x * 2.0f, 0.05f);

        RaycastHit2D result = Physics2D.BoxCast(bottom, hitBoxSize, 0.0f, new Vector3(0.0f, -1.0f), 0.0f, 1 << LayerMask.NameToLayer("Ground"));


        bool grounded = result.collider != null && result.normal.y > 0.9f;

        if (isDashing == true)
        {
            rigidBody.velocity = new Vector2(-dashDif + (43 * plscale) ,0);
        }

        else if (grounded)
        {
            if (Input.GetAxis("Jump") > 0.0f && isDashing == false) 
            {
                rigidBody.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
            }
            
            else if (isDashing == false)
            {
                rigidBody.velocity = new Vector2(speed * h, rigidBody.velocity.y);
            }
        }
        else if (isDashing == false)
        {
            //allow a small amount of movement in the air
            float vx = rigidBody.velocity.x;
            if (h * vx < airControlMax)
                rigidBody.AddForce(new Vector2(h * airControlForce, 0));
        }

        if (Input.GetKey("left ctrl") == true && h != 0 && running == false && isDashing == false)
        {
            isDashing = true;
            running = false;
            dashDist = transform.position.x + (10 * plscale);
        }
        else if (Input.GetKey("left ctrl") && h != 0 && running == true && isDashing == false)
        {
            speed = 13;
            isDashing = false;
        }
        else if (Input.GetKey("left ctrl") == false && h != 0 && running == true && isDashing == false)
        {
            speed = 7;
            isDashing = false;
            if (grounded == true)
            {
                running = false;
            }

        }
        else if (Input.GetKey("left ctrl") == false && h == 0 && running == true && isDashing == false)
        {
            running = false;
            isDashing = false;
            if (grounded == true)
            {
                running = false;
            }

        }


    }

}

