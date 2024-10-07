using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetMasterVolume (float volume)
    {
        audioMixer.SetFloat("mastervolume", volume);
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxvolume", volume);
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicvolume", volume);
    }
}
