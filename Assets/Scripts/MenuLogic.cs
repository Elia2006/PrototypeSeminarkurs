using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        File.Delete(Application.persistentDataPath + "/player.json");
        Time.timeScale = 1f;
        SceneManager.LoadScene("SecondIntro");
        
    }

    public void LoadGame()
    {

        PlayerPrefs.SetInt("laden", 1);
        Time.timeScale = 1f;
        SceneManager.LoadScene("SecondIntro");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
