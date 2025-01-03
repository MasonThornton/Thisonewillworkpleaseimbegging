using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform attachedPlayer;
     Camera thisCamera;
    AudioListener audiolistener;
   public CameraChange camerachange;
    public float blendAmount = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        thisCamera = GetComponent<Camera>();
        audiolistener = GetComponent<AudioListener>();
    }

    // Update is called once per frame
    void Update()
    {
        // float steve = 10;
        //  while (steve == 10)
        //   {
        //      Debug.Log("HA");
        //      steve = steve + 1;
        //  }
        if (camerachange.cameraswap == false)
        {
            Vector3 player = attachedPlayer.transform.position;
            Vector3 newCamPos = player * blendAmount + transform.position * (1.0f - blendAmount);

            transform.position = new Vector3(newCamPos.x, newCamPos.y, transform.position.z);
        }
        else
        {
            thisCamera.enabled = false;
            audiolistener.enabled = false;
            
        }


    }
}
