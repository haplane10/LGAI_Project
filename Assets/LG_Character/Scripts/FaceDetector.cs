using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using OpenCvSharp;

public class FaceDetector : MonoBehaviour
{
    //WebCamTexture _webCamTexture;
    //CascadeClassifier _cascade;
    //OpenCvSharp.Rect _detactedFace;
    //Vector2 _facePosition;

    //[SerializeField] Transform face;
    //Vector3 nectRot;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    WebCamDevice[] devices = WebCamTexture.devices;

    //    _webCamTexture = new WebCamTexture(devices[0].name);
    //    _webCamTexture.Play();

    //    _cascade = new CascadeClassifier(Application.dataPath + @"/LG_Character/Face_Detector/haarcascade_frontalface_default.xml");

    //    nectRot = face.localEulerAngles;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //GetComponent<Renderer>().material.mainTexture = _webCamTexture;
    //    Mat _frame = OpenCvSharp.Unity.TextureToMat(_webCamTexture);
        
    //    FindNewFace(_frame);
    //    Display(_frame);
    //}

    //void FindNewFace(Mat _frame)
    //{
    //    var _faces = _cascade.DetectMultiScale(_frame, 1.1d, 2, HaarDetectionType.ScaleImage);

    //    if (_faces.Length >= 1)
    //    {
    //        Debug.Log($"{_faces[0].Location}, width: {_frame.Width}, height: {_frame.Height}");
    //        face.localEulerAngles = nectRot + new Vector3(0, ((_frame.Width * 0.5f) - _faces[0].Location.X) * 0.5f, 0);
    //        _detactedFace = _faces[0];
    //    }
    //}

    //void Display(Mat _frame)
    //{
    //    if (_detactedFace != null)
    //    {
    //        _frame.Rectangle(_detactedFace, new Scalar(250, 0, 0), 2);
    //    }

    //    Texture newTexture = OpenCvSharp.Unity.MatToTexture(_frame);
    //    GetComponent<Renderer>().material.mainTexture = newTexture;
    //}
}
