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
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
using System.Runtime.CompilerServices;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    public AudioSource coinSound;
    Rigidbody2D rigidBody;
    float speed = 7.0f;
    private float jumpForce = 17.0f;
    public float dashForce = 100000.0f;
    public static float pv;
    public bool isDashing = false;

    public bool dashTimeDone = false;

    public float dashDist = 25;
    public float dashGreatLessChecked = 0;
    public float dashDif;
    private float airControlForce = 10.0f;
    private float airControlMax = 6.5f;
    public static bool dead = false;
   
    public static float py;
    public static float plscale = -1;
    public static List<float> PlayerInventory = new List<float>();
    public bool running = false;
    public GameObject Plunger;
    public Plunger Pl;
    public bool safeToShoot = false;
    // firing variables
    public static bool fire = true;
    public static bool shooting = false;
    public static float plungerRotation = 0;
    public static bool PlungerRemove = false;
    // used for coyote time
   bool coyoteground = false;
 bool grounded = false;


    Vector2 boxExtents;
    public TMP_Text messageText;
    public TextUpdater textupdater;

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




            textupdater.score += 100;

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
            DestroySelf();
            gameObject.SetActive(false);

        }
        {

        }
        float blinkVal = Random.Range(0.0f, 1900.0f);
        if (blinkVal < 1.0f)
        {
            animator.SetTrigger("blinktrigger");
        }

        float h = Input.GetAxis("Horizontal");
        if (Input.GetAxis("Horizontal") * transform.localScale.x < 0.0f)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }


        

  
        plscale = Mathf.Clamp(Mathf.RoundToInt(transform.localScale.x), -1, 1);
        float xSpeed = Mathf.Abs(rigidBody.velocity.x);
        animator.SetFloat("xspeed", xSpeed);
        float ySpeed = Mathf.Abs(rigidBody.velocity.y);
        animator.SetFloat("yspeed", ySpeed);

      
        if (Input.GetKeyDown("f") && plscale != 0)
        {
            StartCoroutine(Fire());

        }
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))


        {
            

            
            dashTimeDone = true;
          
            
        
  }
        if (collision.gameObject.tag == "spike")
        {


            dead = true;

        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {


        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))


        {



            dashTimeDone = false;


        }
    }


    void FixedUpdate()
        {
            dashDif = transform.position.x - dashDist;
       





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


            // super complex code that checks if im dashing, whether or not it needs to check for greater or less and if im close to my approximate destination

            if (dashDif > 1.1 && dashGreatLessChecked == 1 && isDashing == true || dashDif < 1.1 && isDashing == true && dashGreatLessChecked == 2 || isDashing == true && dashTimeDone == true  )
            {
                isDashing = false;
                running = true;
                rigidBody.velocity = new Vector2(0, 0);
                dashTimeDone = false;
            Debug.Log("hieaaaa");

        }








            /*if (h != 0.0f)
            {
                rigidBody.velocity = new Vector2(h * speed, 0.0f);
            }*/

            //Check if we are on the ground
            Vector2 bottom = new Vector2(transform.position.x, transform.position.y - boxExtents.y);

            Vector2 hitBoxSize = new Vector2(boxExtents.x * 2.0f, 0.05f);



            RaycastHit2D result = Physics2D.BoxCast(bottom, hitBoxSize, 0.0f, new Vector3(0.0f, -1.0f), 0.0f, 1 << LayerMask.NameToLayer("Ground"));



            // this code exists to make jumping more forgiving, similar to other 2d platformers it gives slight brieving room before the game stops you from jumping

            coyoteground = result.collider != null && result.normal.y > 0.7f;
            if (coyoteground == false)
            {
                StartCoroutine(CoyoteTimer());
            }
            else
            {
                grounded = true;
            }




            if (isDashing == true)
            {

                rigidBody.velocity = new Vector2(-dashDif + (43 * plscale), 0);
            }

         



            else if (grounded == true)
            {
                if (Input.GetAxis("Jump") > 0.0f && isDashing == false)
                {
                    // stops us from infinitely jumping due to coyote time
                    coyoteground = false;
                    grounded = false;

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
                if (h * vx < airControlMax && isDashing == false)
            {
                    rigidBody.AddForce(new Vector2(h * airControlForce, 0));
            }
        }

            // input code for dashing
            if (Input.GetKey("left ctrl") == true && h != 0 && running == false && isDashing == false)
            {
                isDashing = true;
                running = false;
                dashDist = transform.position.x + (10 * plscale);
            }
            else if (Input.GetKey("left ctrl") && h != 0 && running == true && isDashing == false && grounded == true)
            {
                speed = 13;
         
                isDashing = false;
            }
            else if (Input.GetKey("left ctrl") == false && h != 0 && running == true && isDashing == false && grounded == true)
            {
                speed = 7;
                isDashing = false;
                if (grounded == true)
                {
                    running = false;
                }

            }
            else if (Input.GetKey("left ctrl") == false && h == 0 && running == true && isDashing == false && grounded == true)
            {
                running = false;
                isDashing = false;
                if (grounded == true)
                {
                    running = false;
                }

            }

            if (fire == true && shooting == true)
            {
                Instantiate(Plunger, new Vector2(transform.position.x + (1.0f * plscale), transform.position.y), Quaternion.identity);
                fire = false;
                shooting = false;
                PlungerRemove = false;
                plungerRotation = plscale;
            }
        }
    
    
      







    IEnumerator CoyoteTimer()
    {
        yield return new WaitForSeconds(1.5f);
        grounded = coyoteground;
    }
    public void DestroySelf()
    {
        PlungerRemove = true;
  
    }

    // checks if we can shoot an object forward
    IEnumerator Fire()
    {
        Vector2 hitBoxSize = new Vector2(5.0f * transform.localScale.x, 0.05f);

      
    
        RaycastHit2D result2 = Physics2D.Raycast(transform.position,Vector2.right * 3,1 * transform.localScale.x,1 << LayerMask.NameToLayer("Ground"));
        bool safeToShoot = result2.normal != null && result2.normal.x != 0;
      
        yield return null;
  
        if (safeToShoot == false)
        {
            shooting = true;


        }
    }

}








