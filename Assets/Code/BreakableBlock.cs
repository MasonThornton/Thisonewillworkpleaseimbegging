using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public PlayerController PlayerController;
    public bool Plunged = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerController.py < transform.position.y || collision.gameObject.tag == "mace2")
        {
            if (Plunged == true)
            {
                PlayerController.DestroySelf();
            }
            Destroy(gameObject);
           
        }
        if (collision.gameObject.tag == "Player" && PlayerController.py < transform.position.y)
        {
            Plunged = true;
        }


    }
    private void Update()
    {
        if (PlayerController.fire == true)
        {
            Plunged = false;
        }
    }
}
