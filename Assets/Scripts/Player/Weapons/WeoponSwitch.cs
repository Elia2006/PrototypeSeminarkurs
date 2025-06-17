using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeoponSwitch : MonoBehaviour
{
    [SerializeField] GameObject Weapon1;
    [SerializeField] GameObject Weapon2;
    [SerializeField] GameObject Weapon3;

    public SteamIntegration si;
    public Gun gun;
    public SMG smg;

    private GameObject ammoText;
    private int currentKey = 1;

    [SerializeField] AudioSource switchSound;
    // Start is called before the first frame update
    void Start()
    {
        Weapon1.SetActive(true);
        Weapon2.SetActive(false);
        Weapon3.SetActive(false);
        ammoText.SetActive(false);
    }

    void Awake()
    {
        Weapon1.SetActive(true);
        Weapon2.SetActive(true);
        Weapon3.SetActive(true);
        ammoText = GameObject.Find("AmmoText");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1f)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                currentKey++;
                switchSound.Play();
                if (currentKey > 3)
                {
                    currentKey = 1;
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                currentKey--;
                switchSound.Play();
                if (currentKey < 1)
                {
                    currentKey = 3;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) && currentKey != 1)
            {
                currentKey = 1;
                switchSound.Play();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && currentKey != 2)
            {
                currentKey = 2;
                switchSound.Play();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && currentKey != 3)
            {
                currentKey = 3;
                switchSound.Play();
            }
        }
        
    

        if(currentKey == 1)
        {
            Weapon1.SetActive(true);
            Weapon2.SetActive(false);
            Weapon3.SetActive(false);
            ammoText.SetActive(false);
        } else if(currentKey == 2)
        {
            Weapon1.SetActive(false);
            Weapon2.SetActive(true);
            Weapon3.SetActive(false);
            ammoText.SetActive(true);
        } else if (currentKey == 3)
        {
            Weapon1.SetActive(false);
            Weapon2.SetActive(false);
            Weapon3.SetActive(true);
            ammoText.SetActive(true);
        }

        int totalAmmo = smg.availableAmmo + smg.loadedAmmo + gun.loadedAmmo + gun.availableAmmo;

        if (totalAmmo > 1000 && !si.IsThisAchievementUnlocked("ACH_FULL_AMMO"))
        {
            si.UnlockAchievements("ACH_FULL_AMMO");
        }

    }
}
