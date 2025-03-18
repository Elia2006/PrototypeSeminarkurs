using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondIntroManager : MonoBehaviour
{
    [SerializeField] GameObject img;
    private void Start()
    {
        img.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
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
