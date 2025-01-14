using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Key : MonoBehaviour
{
    public float Identity;

    // Start is called before the first frame update

    void Start()
    {

     
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
           //if the player collides then it adds it's identity to the players inventory
            if (coll.gameObject.tag == "Player")
            {
            MainManager.Instance.ItemSound();
            PlayerController.PlayerInventory.Add(Identity);
            Destroy(gameObject);

        }
            }
        }
 