using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{

    public bool cameraswap = false;
    public void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Player")
        {
            cameraswap = true;
        }
    }
}
