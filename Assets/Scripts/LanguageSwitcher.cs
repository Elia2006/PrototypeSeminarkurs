using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSwitcher : MonoBehaviour
{
    public bool lang;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToGerman() 
    {
        lang = true;
    }

    public void SwitchToEnglish() 
    {
        lang = false;
    }
}
