using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        File.Delete(Application.persistentDataPath + "/player.json");
        SceneManager.LoadScene("Final");
        
    }

    public void LoadGame()
    {
        PlayerPrefs.SetInt("laden", 1);
        SceneManager.LoadScene("Final");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
