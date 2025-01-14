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
    private bool DoorOpening = false;
    public PlayerController PlayerController;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
  

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // if the object collides with player and the player controllers inventory has it's identity then it deletes itself
        if (collision.gameObject.tag == "Player" && PlayerController.PlayerInventory.Contains(Identity))
        {
            if (Plunged == true)
            {
                PlayerController.DestroySelf();

            }
            DoorOpening = true;
           

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
       if (DoorOpening == true)
        {
            MainManager.Instance.DoorSound();
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - (5.75f * Time.deltaTime));
        }
       if (spriteRenderer.color.a <= 0)
        {
            Destroy(gameObject);

        }






    }
}

    // Update is called once per frame


