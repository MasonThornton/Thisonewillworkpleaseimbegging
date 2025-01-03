using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCam : MonoBehaviour
{
   public CameraChange camerachange;
     Camera thisCamera;
    AudioListener audiolistener;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {
        thisCamera = GetComponent<Camera>();
        audiolistener = GetComponent<AudioListener>();
    }
    void Update()
    {
        if (camerachange.cameraswap == true)
        {
            thisCamera.enabled = true;
            audiolistener.enabled = true;
        }
    }

}
