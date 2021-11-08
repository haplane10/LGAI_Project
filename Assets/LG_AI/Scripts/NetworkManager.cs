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
        data.media = vido

        //var _dirName = Path.GetDirectoryName(videoFilePath);
        //var _fileName = Path.GetFileName(videoFilePath);
        //var _fileByte = File.ReadAllBytes(videoFilePath);
        //var _fileByte = File.ReadAllBytes("E:/Unity3D/LGAI_Project/recording_2021_11_09_00_13_03_261.mp4");
        var _fileByte = File.ReadAllBytes("D:/Test/1.png");


        // FileInfo _info = Directory.GetFiles(videoFilePath);
        Debug.Log($"{_fileByte.Length}");
        return _fileByte;
    }

    IEnumerator co_UploadVideo()
    {
        yield return null;
        //        GET http://3.34.113.181/v1/files
        //POST http://3.34.113.181/v1/files 
        // -media(multipart)

        //string _json = JsonUtility.ToJson(ri);
        string _json = "{\"media\":\"recording_2021_11_09_00_13_03_261.mp4\"}";

    //string url = "http://3.34.113.181/v1/files - media(multipart)";
    string url = "http://3.34.113.181/v1/files";

        byte[] bytes = GetFileData();
       // byte[] bytes = System.Text.Encoding.UTF8.GetBytes(_json);

        //UnityWebRequest www = UnityWebRequest.Post(url, "");
        UnityWebRequest www = UnityWebRequest.Put(url, bytes);
        //www.uploadHandler = new UploadHandlerRaw(bytes);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();
        if (!www.isNetworkError)
        {
            Debug.Log("www.text : " + www.downloadHandler.text + ", www.error : " + www.error);
        }
        else
        {
            Debug.LogError("error : " + www.error);
        }

        //RoomInfoResult rir = JsonUtility.FromJson<RoomInfoResult>(www.downloadHandler.text);
        //if (rir.error.Count != 0)
        //{
        //    Debug.Log("������ ��...");
        //}
        //else
        //{
        //    Debug.Log("���� ����ΰ�?");
        //}


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
                // ������ ��� 
                // TODO : �˾� �޼����� ����� �����ϴ� �� �ƴұ�?
                Debug.Log(pages[page] + ": Error: " + www.error);
            }
            else
            {
                // ���� �۵��� ���
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