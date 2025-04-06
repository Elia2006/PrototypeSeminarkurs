using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SecondIntroManager : MonoBehaviour
{
    public string url;
    public AudioSource source;

    public VideoPlayer video;

    [SerializeField] GameObject img;
    private void Start()
    {
        img.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;

        url = "File://" + Application.streamingAssetsPath + "/" + "Seminarkurs intro part 2.mp4";
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

    public float targetTime = 17.0f;

    private void Update()
    {


        targetTime -= Time.deltaTime;
        if (targetTime < 16.9)
        {
            img.SetActive(false);
        }
        if (targetTime <= 0.0f)
        {
            timerEnded();
        }

    }

    void timerEnded()
    {
        SceneManager.LoadScene("Final");
    }
}
