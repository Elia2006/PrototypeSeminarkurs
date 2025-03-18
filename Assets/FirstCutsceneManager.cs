using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstCutsceneManager : MonoBehaviour
{
    [SerializeField] GameObject black;
    private void Start()
    {
        black.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public float targetTime = 33.0f;

    private void Update()
    {
        

        targetTime -= Time.deltaTime;

        if (targetTime <= 32.9f)
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
        SceneManager.LoadScene("StartMenu");
    }
}
