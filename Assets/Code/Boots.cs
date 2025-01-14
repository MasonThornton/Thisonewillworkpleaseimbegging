using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Player")


        {
            MainManager.Instance.ItemSound();
            Destroy(gameObject);
            MainManager.Instance.HasDash = true;

        }
    }
}
