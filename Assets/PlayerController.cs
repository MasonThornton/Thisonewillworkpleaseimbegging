using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidBody;
    public float speed = 5.0f;
    public float jumpForce = 8.0f;
    public float airControlForce = 10.0f;
    public float airControlMax = 1.5f;
    public float wait = 5;
    
    Vector2 boxExtents;

    //use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxExtents = GetComponent<BoxCollider2D>().bounds.extents;
        animator = GetComponent<Animator>();
    }

    //Update is called once per frame
    void Update()

    {

        if (rigidBody.velocity.x * transform.localScale.x < 0.0f)
            transform.localScale = new Vector3(-transform.localScale.x,
           transform.localScale.y, transform.localScale.z);
        float xSpeed = Mathf.Abs(rigidBody.velocity.x);
        animator.SetFloat("xspeed", xSpeed);
        float ySpeed = Mathf.Abs(rigidBody.velocity.y);
        animator.SetFloat("yspeed", ySpeed);

        if (xSpeed == 0)
        {
            wait = 0;

            animator.SetFloat("wait",wait);
        }
        else
        {
            wait = 5;
            animator.SetFloat("wait", wait);
        }

    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
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
            else
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
