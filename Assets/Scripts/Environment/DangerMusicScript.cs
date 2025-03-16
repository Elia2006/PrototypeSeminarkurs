using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;
using UnityEngine;

public class DangerMusicScript : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] HUD playerHUD;

    private readonly float maxVolume = 0.3f;
    private Coroutine start;
    private Coroutine stop;
    private String currentTrackPlaying = "";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Danger());
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHUD.anyAttacking)
        {
            Debug.Log("hello");
            currentTrackPlaying = "Danger";
        }else
        {
            currentTrackPlaying = "";
        }

        
    }


    IEnumerator Danger()
    {
        while(true)
        {
            if(currentTrackPlaying.Equals("Danger"))
            {
                Debug.Log("hfe");
                while(music.volume < maxVolume)
                {
                    music.volume += 2 * Time.deltaTime;
                }
                music.volume = maxVolume;
            }else
            {
                while(music.volume > 0)
                {
                    music.volume -= 1 * Time.deltaTime;
                }
                music.volume = 0;
            }
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator StartMusic()
    {
        while(music.volume < maxVolume)
        {
            music.volume += 0.01f;
            yield return new WaitForSeconds(0.05f);
        }
        music.volume = maxVolume;
        yield return null;
    }
    IEnumerator StopMusic()
    {
        while(music.volume > 0)
        {
            music.volume -= 0.01f;
            yield return new WaitForSeconds(0.2f);
        }
        music.volume = 0;
        yield return null;
    }
}
