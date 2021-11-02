/* 
*   NatCorder
*   Copyright (c) 2021 Yusuf Olokoba
*/

namespace NatSuite.Examples {

    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;
    using Recorders;
    using Recorders.Clocks;

    public class WebCam : MonoBehaviour {

        [Header(@"UI")]
        public RawImage rawImage;
        public AspectRatioFitter aspectFitter;
        public Text pathText;
        private WebCamTexture webCamTexture;
        private MP4Recorder recorder;
        private IClock clock;
        private bool recording;
        private Color32[] pixelBuffer;
        private bool camAvailable;
        public int videoWidth;
        public int videoHeight;
        private Texture defaultBackground;
        #region --Recording State--

        public void StartRecording () {
            // Start recording
            defaultBackground = rawImage.texture;
            clock = new RealtimeClock();
            recorder = new MP4Recorder(webCamTexture.width, webCamTexture.height, 30);
            pixelBuffer = webCamTexture.GetPixels32();
            recording = true;
        }

        public async void StopRecording () {
            // Stop recording
            recording = false;
            var path = await recorder.FinishWriting();
            // Playback recording
            Debug.Log($"Saved recording to: {path}");
            pathText.text = path;
            Handheld.PlayFullScreenMovie($"file://{path}");
        }
        #endregion


        #region --Operations--

        void Start()
        {
            WebCamDevice[] devices = WebCamTexture.devices;

            if (devices.Length == 0)
            {
                Debug.Log("No Camera Detacted!!");
                camAvailable = false;
                return;
            }


            Debug.Log($"Number of Camera : {devices.Length}");
#if UNITY_EDITOR
            webCamTexture = new WebCamTexture(devices[0].name, videoWidth, videoHeight);
#elif UNITY_ANDROID
        //for(int i = 0; i < devices.Length; i++)
        //{
        //    if (!devices[i].isFrontFacing)
        //    {
                webCamTexture = new WebCamTexture(devices[0].name, videoWidth, videoHeight);
        //    }
        //}
#elif UNITY_IOS
        for(int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
            {
                webCamTexture = new WebCamTexture(devices[0].name, videoWidth, videoHeight);
            }
        }
#endif

            if (webCamTexture == null)
            {
                Debug.Log("Unable to find back camera.");
                return;
            }

            webCamTexture.Play();
            rawImage.texture = webCamTexture;

            camAvailable = true;
        }

        void Update () {
            if (!camAvailable) return;

            // Record frames from the webcam
            if (recording && webCamTexture.didUpdateThisFrame) {
                webCamTexture.GetPixels32(pixelBuffer);
                recorder.CommitFrame(pixelBuffer, clock.timestamp);
            }

            if (webCamTexture == null) return;
            float ratio = (float)webCamTexture.width / (float)webCamTexture.height;            
            aspectFitter.aspectRatio = ratio;

            float scaleY = webCamTexture.videoVerticallyMirrored ? -1f : 1f;
            rawImage.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

            int orient = -webCamTexture.videoRotationAngle;
            rawImage.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
        }
        #endregion
    }
}