using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollider : MonoBehaviour
{
    public static bool inside = false;

    // Start is called before the first frame update


    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "ground" || coll.gameObject.tag == "door")
        {
            inside = true;
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "ground" || coll.gameObject.tag == "door")
        {
            inside = false;
        }
    }

    }
