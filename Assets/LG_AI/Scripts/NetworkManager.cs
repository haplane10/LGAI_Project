using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;

    private string videoFilePath;
    private Data data = new Data();
    public string VideoFilePath { get => videoFilePath; set => videoFilePath = value; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnUploadButtonClick()
    {
        StartCoroutine(co_UploadVideo());
    }

    public byte[] GetFileData()
    {
        //data.media = vido

        //var _dirName = Path.GetDirectoryName(videoFilePath);
        //var _fileName = Path.GetFileName(videoFilePath);
        //var _fileByte = File.ReadAllBytes(videoFilePath);
        //var _fileByte = File.ReadAllBytes("E:/Unity3D/LGAI_Project/recording_2021_11_09_00_13_03_261.mp4");
        var _fileByte = File.ReadAllBytes(Application.dataPath + "/123.mp4");


        // FileInfo _info = Directory.GetFiles(videoFilePath);
        Debug.Log($"{_fileByte.Length}");
        return _fileByte;
    }

    IEnumerator co_UploadVideo()
    {
        //WWW localFile = new WWW("file:///" + "D:/Video/123.mp4");
        //yield return localFile;
        //if (localFile.error == null)
        //    Debug.Log("Loaded file successfully");
        //else
        //{
        //    Debug.Log("Open file error: " + localFile.error);
        //    yield break; // stop the coroutine here
        //}

        //WWWForm postForm = new WWWForm();

        //postForm.AddBinaryData("media", localFile.bytes, localFile.text);
        //WWW upload = new WWW("http://3.34.113.181/v1/files", postForm);
        //yield return upload;
        //if (upload.error == null)
        //    Debug.Log("upload done :" + upload.text);
        //else
        //    Debug.Log("Error during upload: " + upload.error);


        //    yield return null;
        //        GET http://3.34.113.181/v1/files
        //POST http://3.34.113.181/v1/files 
        // -media(multipart)

        //string _json = JsonUtility.ToJson(ri);
        //string _json = "{\"media\":\"recording_2021_11_09_00_13_03_261.mp4\"}";

        //string url = "http://3.34.113.181/v1/files - media(multipart)";
        string url = "http://3.34.113.181/v1/files/";

        byte[] bytes = GetFileData();

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        //formData.Add(new MultipartFormDataSection("media"));
        formData.Add(new MultipartFormFileSection("media", bytes, "123.mp4", "video/mp4"));

        //UnityWebRequest www = UnityWebRequest.Post(url, formData);
        //yield return www.SendWebRequest();

        //if (www.result != UnityWebRequest.Result.Success)
        //{
        //    Debug.Log(www.error);
        //}
        //else
        //{
        //    Debug.Log("Form upload complete!");
        //}

        //List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        //formData.Add(new MultipartFormDataSection("meida"));
        //formData.Add(new MultipartFormFileSection(bytes));

        //WWWForm form = new WWWForm();
        //form.AddBinaryData("meida", bytes, Application.dataPath + "/112.png");

        //UnityWebRequest www = UnityWebRequest.Post(url, formData);
        //yield return www.SendWebRequest();

        //if (www.result != UnityWebRequest.Result.Success)
        //{
        //    Debug.Log(www.error);
        //}
        //else
        //{
        //    Debug.Log("Form upload complete!");
        //}

        //// byte[] bytes = System.Text.Encoding.UTF8.GetBytes(_json);

        UnityWebRequest www = UnityWebRequest.Post(url, formData);
        //UnityWebRequest www = UnityWebRequest.Put(url, bytes);
        //www.uploadHandler = new UploadHandlerFile(Application.dataPath + "/112.png");
        //www.downloadHandler = new DownloadHandlerBuffer();
        //www.SetRequestHeader("Content-Type", "multipart/form-data");

        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }

    public void OnDownloadButtonClick()
    {
        StartCoroutine(co_DownloadVideo());
    }

    IEnumerator co_DownloadVideo()
    {
        string url = "http://3.34.113.181/v1/files";
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return www.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            if (www.isNetworkError)
            {
                // 에러일 경우 
                // TODO : 팝업 메세지로 경고가 떠야하는 것 아닐까?
                Debug.Log(pages[page] + ": Error: " + www.error);
            }
            else
            {
                // 정상 작동일 경우
                // Debug.Log(pages[page] + ":\nReceived: " + www.downloadHandler.text);
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    
}

[System.Serializable]
public class Data
{
    public string media;
}
