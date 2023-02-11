using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Room4VideoHandler : MonoBehaviour
{
    [Header("Content Referrences")]
    public GameObject landingPage;
    public VideoPlayer videoPlayer;
    public RenderTexture renderTexture;

    [Header("Animation Referrences")]
    public Animator landingAnim;

    void ReceiveSensorData(string data)
    {
        //Proximity Sensor Control 
        if (data == "")
        {
            PlayContentVideo();
        }
        else
        {
            ContentEnded();
        }
    }

    void PlayContentVideo()
    {
        landingAnim.SetBool("stop", false);
        videoPlayer.Play();
    }

    void ContentEnded()
    {
        landingAnim.SetBool("stop", true);
        videoPlayer.Stop();
        renderTexture.Release();
    }
}

