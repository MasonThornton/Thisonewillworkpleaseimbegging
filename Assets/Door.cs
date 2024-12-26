using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public float Identity;
      Rigidbody2D rigidBody;
    public bool Plunged = false;
    public PlayerController Player;
    // Start is called before the first frame update
  

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // if the object collides with player and the player controllers inventory has it's identity then it deletes itself
        if (collision.gameObject.tag == "Player" && PlayerController.PlayerInventory.Contains(Identity))
        {
            if (Plunged == true)
            {
                Player.DestroySelf();

            }
            Destroy(gameObject);
          

        }

        if (collision.gameObject.tag == "plunger")
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

    // Update is called once per frame


