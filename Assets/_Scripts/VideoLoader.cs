using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoLoader : MonoBehaviour
{
    public string videoName;
    public bool playOnStart = true;
    private VideoPlayer videoPlayer_;

    private void Start()
    {
        videoPlayer_ = GetComponent<VideoPlayer>();
        if (playOnStart)
            Play();
    }

    public void Play()
    {
        videoPlayer_.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoName);
        videoPlayer_.Play();
    }
}
