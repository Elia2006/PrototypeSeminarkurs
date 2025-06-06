using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.Localization.Settings;

public class OutroManager : MonoBehaviour
{
    public string url;
    public AudioSource source;

    public VideoPlayer video;
    [SerializeField] GameObject black;
    private void Start()
    { 

        black.SetActive(true);
        
        Cursor.lockState = CursorLockMode.Locked;

        //LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];

        if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
        {
            //deutsch
            url = "File://" + Application.streamingAssetsPath + "/" + "outro deutsch diesmal wirklich das letzte mal .mp4";
        }
        else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
        {
            //englisch
            url = "File://" + Application.streamingAssetsPath + "/" + "outro englisch diesmal wirklich das letzte mal .mp4";
        }
        video = GameObject.Find("VideoPlayer").GetComponent<VideoPlayer>();
        video.source = VideoSource.Url;
        video.url = url;


        StartCoroutine(PlayVideo());
    }

    private IEnumerator PlayVideo()
    {
        var audioSource = video.GetComponent<AudioSource>();
        video.audioOutputMode = VideoAudioOutputMode.AudioSource;
        video.controlledAudioTrackCount = 1;
        video.EnableAudioTrack(0, true);
        video.SetTargetAudioSource(0, audioSource);

        // Wait until ready
        video.Prepare();
        while (!video.isPrepared)
            yield return null;

        video.Play();


        while (video.isPlaying)
            yield return null;


    }

    public float targetTime = 53.0f;

    private void Update()
    {


        targetTime -= Time.deltaTime;
        if (targetTime < 52.9)
        {
            black.SetActive(false);
        }
       
        if (targetTime <= 0.0f)
        {
            timerEnded();
        }

    }

    void timerEnded()
    {
        if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
        {
            //deutsch
            SceneManager.LoadScene("StartMenu");
        }
        else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
        {
            //englisch
            SceneManager.LoadScene("StartMenuEN");
        }
        
    }
}