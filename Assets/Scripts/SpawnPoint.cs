using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class SpawnPoint : MonoBehaviour
{
    private bool collision = false;
    public bool teleport = false;
    public GameObject Map;
    public bool canMapOpen2;
    public GameObject PressM;
    private TextMeshProUGUI pressMText;

    // Start is called before the first frame update
    void Start()
    {
        pressMText = PressM.GetComponentInChildren<TextMeshProUGUI>();
        PressM.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiateTeleport()
    {
        if (collision) 
        {
            teleport = true;
            collision = false;
        }
    

    }

    private void OnTriggerEnter(Collider other)
    {
        collision = true;
        Map.GetComponent<Map>().canMapOpen = true;
        PressM.SetActive(true);
        if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
        {
            pressMText.text = "und wähle einen Teleporter aus um dich zu teleportieren.";
        }
        else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
        {
            pressMText.text = "and select another Teleporter to fast travel to.";
        }
    }
    private void OnTriggerExit(Collider other)
    {
        collision = false;
        Map.GetComponent<Map>().canMapOpen = false;
        PressM.SetActive(false);
        
    }

}