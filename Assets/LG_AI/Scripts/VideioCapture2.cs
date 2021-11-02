using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideioCapture2 : MonoBehaviour
{
    WebCamTexture deviceCam;

    // Start is called before the first frame update
    void Start()
    {
        if (deviceCam == null)
        {
            deviceCam = new WebCamTexture();
        }

        GetComponent<Renderer>().material.mainTexture = deviceCam;

        if (!deviceCam.isPlaying)
        {
            deviceCam.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
