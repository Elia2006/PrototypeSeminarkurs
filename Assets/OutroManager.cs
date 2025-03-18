using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;
using UnityEngine.SceneManagement;

public class OutroManager : MonoBehaviour
{
    [SerializeField] GameObject black;
    private void Start()
    {
        black.SetActive(true);
        
        Cursor.lockState = CursorLockMode.Locked;
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
        SceneManager.LoadScene("StartMenu");
    }
}