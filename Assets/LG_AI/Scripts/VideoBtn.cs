using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoBtn : MonoBehaviour
{
    public GameObject videoPreviewScreen;
    public data videoData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void OnPlayButtonClick()
    {
        videoPreviewScreen.SetActive(true);
        var _videoPlayer = FindObjectOfType<VideoPlayer>();
        _videoPlayer.url = videoData.url;
        _videoPlayer.Play();
    }
}
