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

public class PlayerController : MonoBehaviour
{
    Animator animator;
    public AudioSource coinSound;
    Rigidbody2D rigidBody;
    float speed = 5.0f;
    public float jumpForce = 8.0f;
    public float dashForce = 100000.0f;
    public double dashTimer = 1.1;
    public bool isDashing = false;
    public bool hasDashed  = false;
    public float airControlForce = 10.0f;
    public float airControlMax = 1.5f;
    public static bool dead = false;
    public static float score = 0;
    public static float py;
    public static float plscale;
    public static List<float> PlayerInventory = new List<float>();
    public bool running = false;




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

        plscale = -transform.localScale.x;

        if (rigidBody.velocity.x * transform.localScale.x < 0.0f)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        float xSpeed = Mathf.Abs(rigidBody.velocity.x);
        animator.SetFloat("xspeed", xSpeed);
        float ySpeed = Mathf.Abs(rigidBody.velocity.y);
        animator.SetFloat("yspeed", ySpeed);

        if (Input.GetKeyDown("f"))
        {



        }


    }






    void FixedUpdate()
    {

        if (dashTimer <= 0)
        {
            dashTimer = 12.1;
            isDashing = false;
            running = true;
            hasDashed = false;
        }
        if (isDashing == true)
        {
            dashTimer -= Time.deltaTime;

        }


        float h = Input.GetAxis("Horizontal");

      
     if (Input.GetKey("left ctrl") && h != 0 && running == true)
        {
            speed = 15;
            isDashing = false;
        }
        else if (Input.GetKey("left ctrl") == false && h != 0 && running == true)
        {
            speed = 5;
            isDashing = false;

        }
        else if (Input.GetKey("left ctrl") == false && h == 0 && running == true)
        {
            running = false;
            isDashing = false;

        }
        else if (Input.GetKey("left ctrl") && h == 0 && running == false)
        {
            running = true;
                 isDashing = false;

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

        if (grounded)
        {
            if (Input.GetAxis("Jump") > 0.0f)
            {
                rigidBody.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
            }
            if (Input.GetKey("left ctrl") == true && h != 0 && running == false && isDashing == false)
            {
                rigidBody.velocity = new Vector2(dashForce, rigidBody.velocity.y);
                rigidBody.velocity = new Vector2(dashForce, rigidBody.velocity.y);
                rigidBody.velocity = new Vector2(dashForce, rigidBody.velocity.y);
                rigidBody.velocity = new Vector2(dashForce, rigidBody.velocity.y);
            }
            else if (isDashing == false)
            {
                rigidBody.velocity = new Vector2(speed * h, rigidBody.velocity.y);
            }
        }
        else
        {
            //allow a small amount of movement in the air
            float vx = rigidBody.velocity.x;
            if (h * vx < airControlMax)
                rigidBody.AddForce(new Vector2(h * airControlForce, 0));
        }

    }
 
}

