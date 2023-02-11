using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GamepadController : MonoBehaviour
{
    [Header("Content Referrences")]
    public GameObject landingPage;
    public VideoPlayer videoPlayer;
    public GameObject[] canvasPrefabs;

    [Header("Animation Referrences")]
    public Animator landingAnim;

    [Range(0,5)]
    int hitCounter;
    bool[] hits;


    private void Start()
    {
        hitCounter = 0;
        hits = new bool[] { false, false, false, false, false };

        videoPlayer.loopPointReached += ContentEnded;
    }

    void Update()
    {
        if (!videoPlayer.isPlaying)
        {
            if (Input.GetKeyDown("joystick button 2") || Input.GetKeyDown(KeyCode.A))
            {
                //hitCounter++;
                hits[0] = true;
            }
            if (Input.GetKeyDown("joystick button 3") || Input.GetKeyDown(KeyCode.S))
            {
                //hitCounter++;
                hits[1] = true;
            }
            if (Input.GetKeyDown("joystick button 5") || Input.GetKeyDown(KeyCode.D))
            {
                //hitCounter++;
                hits[2] = true;
            }
            if (Input.GetKeyDown("joystick button 7") || Input.GetKeyDown(KeyCode.F))
            {
                //hitCounter++;
                hits[3] = true;
            }
            if (Input.GetKeyDown("joystick button 9") || Input.GetKeyDown(KeyCode.G))
            {
               // hitCounter++;
                hits[4] = true;
            }

            if (hits[0] == true && hits[1] == true && hits[2] == true && hits[3] == true && hits[4] == true)
            {
                //renderTexture.Release();
                PlayContentVideo();
                ResetCounters();
            }
        }

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

    void ResetCounters()
    {
        for (int i = 0; i < hits.Length; i++)
        {
            hits[i] = false;
        }
    }

    void PlayContentVideo()
    {
        Debug.Log(hitCounter);
        landingAnim.SetBool("stop", false);
        videoPlayer.Play();
        //landingPage.SetActive(false);
    }

    void ContentEnded(VideoPlayer vp)
    {
        //landingPage.SetActive(true);
        landingAnim.SetBool("stop", true);
        videoPlayer.Stop();
        //renderTexture.Release();

        //hitCounter = 0;
    }
}
