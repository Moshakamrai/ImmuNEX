using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Room1VideoHandler : MonoBehaviour
{
    [Header("Content Referrences")]
    public GameObject landingPage;
    public VideoPlayer videoPlayer;
    public RenderTexture renderTexture;

    [Header("Animation Referrences")]
    public Animator landingAnim;

    private void Start()
    {
        videoPlayer.loopPointReached += ContentEnded;
    }

    void Update()
    {
        //Mouse Control 
        if (Input.GetMouseButtonDown(0))
        {
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Stop();
                videoPlayer.Play();
            }
            else
            {
                PlayContentVideo();
            }
        }
    }

    void PlayContentVideo()
    {
        landingAnim.SetBool("stop", false);
        videoPlayer.Play();
    }

    void ContentEnded(VideoPlayer vp)
    {
        landingAnim.SetBool("stop", true);
        videoPlayer.Stop();
        renderTexture.Release();
    }
}
