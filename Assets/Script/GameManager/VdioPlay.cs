using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VdioPlay : MonoBehaviour

{

    void Start()
    {
        VideoPlayer video = GetComponent<VideoPlayer>();

        video.loopPointReached += End;
           
    }

    void End(VideoPlayer videoPlayer)
    {
        Debug.Log("视频播放完成~！");

        gameObject.SetActive(false);
    }
}
