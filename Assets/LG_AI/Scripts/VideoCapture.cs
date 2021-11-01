using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoCapture : MonoBehaviour
{
    private bool camAvailable;
    private WebCamTexture backCamera;
    private Texture defaultBackground;

    public RawImage background;
    public AspectRatioFitter fit;

    public Text resolutionOut;

    public int videoWidth;
    public int videoHeight;
    // Start is called before the first frame update
    void Start()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.Log("No Camera Detacted!!");
            camAvailable = false;
            return;
        }


        Debug.Log($"Number of Camera : {devices.Length}");
#if UNITY_EDITOR
        backCamera = new WebCamTexture(devices[0].name, videoWidth, videoHeight);
#elif UNITY_ANDROID
        //for(int i = 0; i < devices.Length; i++)
        //{
        //    if (!devices[i].isFrontFacing)
        //    {
                backCamera = new WebCamTexture(devices[0].name, videoWidth, videoHeight);
        //    }
        //}
#elif UNITY_IOS
        for(int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
            {
                backCamera = new WebCamTexture(devices[0].name, videoWidth, videoHeight);
            }
        }
#endif

        if (backCamera == null)
        {
            Debug.Log("Unable to find back camera.");
            return;
        }

        resolutionOut.text = $"{backCamera.width} x {backCamera.height}";
        backCamera.Play();
        background.texture = backCamera;

        camAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!camAvailable) return;

        float ratio = (float)backCamera.width / (float)backCamera.height;
        fit.aspectRatio = ratio;

        float scaleY = backCamera.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -backCamera.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }
}
