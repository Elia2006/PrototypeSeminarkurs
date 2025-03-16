using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class DangerMusicScript : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] HUD playerHUD;

    private readonly float maxVolume = 0.3f;
    private Coroutine start;
    private Coroutine stop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHUD.anyAttacking && start == null)
        {
            if(stop != null)
            {
                StopCoroutine(stop);
                stop = null;
            }
            
            start = StartCoroutine(StartMusic());
        }else if(!playerHUD.anyAttacking && stop == null)
        {
            if(start != null)
            {
                StopCoroutine(start);
                start = null;
            }
            
            stop = StartCoroutine(StopMusic());
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
