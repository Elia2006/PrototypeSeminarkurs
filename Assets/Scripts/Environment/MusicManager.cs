using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource dangerMusic;
    [SerializeField] AudioSource preBossMusic;
    [SerializeField] AudioSource bossMusic;
    [SerializeField] HUD playerHUD;
    [SerializeField] IsColliding[] preBossColliders;
    private GameObject Boss;

    private Coroutine start;
    private Coroutine stop;
    private String currentTrackPlaying = "";

    // Start is called before the first frame update
    void Start()
    {
        Boss = GameObject.Find("boss als 1 objekt");
        StartCoroutine(Danger());
    }

    // Update is called once per frame
    void Update()
    {
        if(Boss != null && Boss.GetComponent<Boss_new>().enabled == true && Boss.GetComponent<Boss_new>().isActivated == true)
        {
            currentTrackPlaying = "BossMusic";
        }else if(playerHUD.anyAttacking)
        {
            currentTrackPlaying = "Danger";
        }else if(preBossColliders[0].isColliding || preBossColliders[1].isColliding)
        {
            currentTrackPlaying = "PreBoss";
        }
        else
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
                FadeIn(dangerMusic, 0.3f);
                
            }else
            {
                FadeOut(dangerMusic, 0.3f);
            }
            if(currentTrackPlaying.Equals("PreBoss"))
            {
                FadeIn(preBossMusic, 0.5f);
            }else
            {
                FadeOut(preBossMusic, 0.5f);
            }
            if(currentTrackPlaying.Equals("BossMusic"))
            {
                FadeIn(bossMusic, 0.3f);
            }else
            {
                FadeOut(bossMusic, 0.2f);
            }


            yield return new WaitForSeconds(0.1f);
        }
    }

    private void FadeIn(AudioSource music, float maxVolume)
    {
        if(music.volume < maxVolume)
        {
            music.volume += maxVolume / 20f;
        }else if(music.volume > maxVolume)
        {
            music.volume = maxVolume;
        }
    }

    private void FadeOut(AudioSource music, float maxVolume)
    {
        if(music.volume > 0)
        {
            music.volume -= maxVolume / 40f;
        }else if(music.volume < 0)
        {
            music.volume = 0;
        }
    }

}
