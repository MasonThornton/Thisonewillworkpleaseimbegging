using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerController.py < transform.position.y)
        {
            Destroy(gameObject);
            PlayerController.score += 10;
        }
    }
}
