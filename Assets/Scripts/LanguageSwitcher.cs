using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class LanguageSwitcher : MonoBehaviour
{
    private bool active = false;
    [SerializeField] GameObject imageGER;
    [SerializeField] GameObject imageEN;
    public void ChangeLocale()
    {
        if (active)
        {
            return;
        }
        if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0]) 
        {
            StartCoroutine(SetLocale(1));
        } 
        else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1]) 
        {
            StartCoroutine(SetLocale(0)); 
        }
        
    }

    IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        active = false;
    }

    public void ChangeLocaleToGerman()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        imageGER.SetActive(true);
        imageEN.SetActive(false);
        
    }

    public void ChangeLocaleToEnglish()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
        imageEN.SetActive(true);
        imageGER.SetActive(false);
        
    }

    public void LoadCutscene()
    {
        if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
        {
            SceneManager.LoadScene("FirstCutscene");
        }
        else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
        {
            SceneManager.LoadScene("FirstCutsceneEN");
        }
        
    }
}
