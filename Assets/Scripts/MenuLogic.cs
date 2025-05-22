using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Localization.Settings;
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

        if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
        {
            //deutsch
            SceneManager.LoadScene("SecondIntro");
        }
        else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
        {
            //englisch
            SceneManager.LoadScene("SecondIntroEN");
        }
        
        
    }

    public void LoadGame()
    {

        PlayerPrefs.SetInt("laden", 1);
        Time.timeScale = 1f;
        if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
        {
            //deutsch
            SceneManager.LoadScene("SecondIntro");
        }
        else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
        {
            //englisch
            SceneManager.LoadScene("SecondIntroEN");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
