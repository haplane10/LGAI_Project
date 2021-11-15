using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;

    private string videoFilePath;
    public ShoeData shoeData;
    [SerializeField] Image[] uploadImages;
    [SerializeField] TextMeshProUGUI uploadProgress;
    [SerializeField] GameObject videoPrefab;
    public bool isShowVideoList = false;
    public string VideoFilePath { get => videoFilePath; set => videoFilePath = value; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (isShowVideoList)
        {
            OnDownloadButtonClick();
        }
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
        //var _dirName = Path.GetDirectoryName(videoFilePath);
        //var _fileName = Path.GetFileName(videoFilePath);
        var _fileByte = File.ReadAllBytes(videoFilePath);

        Debug.Log($"{_fileByte.Length}");

        return _fileByte;
    }

    IEnumerator co_UploadVideo()
    {
        //GET http://3.34.113.181/v1/files
        //POST http://3.34.113.181/v1/files - media(multipart)

        string url = "http://3.34.113.181/v1/files/";

        byte[] bytes = GetFileData();

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        //formData.Add(new MultipartFormDataSection("media"));
        formData.Add(new MultipartFormFileSection("media", bytes, videoFilePath, "video/mp4"));

        UnityWebRequest www = UnityWebRequest.Post(url, formData);

        var op = www.SendWebRequest();

        while (!www.isDone)
        {
            float progress = Mathf.Clamp01(www.uploadProgress / 0.9f);
            uploadImages[0].fillAmount = progress;
            uploadImages[1].fillAmount = progress;

            progress = Mathf.Round(progress * 100);
            uploadProgress.text = progress + "%";
            
            yield return new WaitForEndOfFrame();
        }

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }

        www.Dispose();

        ChangeScene(5);
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

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                var json = www.downloadHandler.text;
                Debug.Log(json);

                shoeData = JsonUtility.FromJson<ShoeData>(www.downloadHandler.text);
            }
        }

        foreach (var _data in shoeData.data)
        {
            var _videoPrefab = Instantiate(videoPrefab, videoPrefab.transform.parent);
            _videoPrefab.SetActive(true);
            var _videoBtn = _videoPrefab.GetComponent<VideoBtn>();
            _videoBtn.videoData = _data;
            var _videoName = _videoPrefab.GetComponentInChildren<Text>();
            _videoName.text = _data.id.ToString();
        }
    }

    public void ChangeScene(int _idx)
    {
        GameManager.Instance.ChangeScene(_idx);
    } 
}

[System.Serializable]
public class ShoeData
{
    public data[] data;
}
[System.Serializable]
public class data
{
    public int id;
    public string platform_key;
    public string url;
    public long size;
    public string rdt;
    public string udt;
}


