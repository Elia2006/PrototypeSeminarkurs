using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Localization.Settings;

public class LanguageSwitcher : MonoBehaviour
{
    private bool active = false;
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
}
