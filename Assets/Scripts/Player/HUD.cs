using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;


public class HUD : MonoBehaviour
{
    //crowbar_ach

    public bool sandcrab = false;
    public bool enemyrange = false;
    public bool enemymelee = false;
    public bool scavengerrange = false;
    public bool scavengermelee = false;

    public float totalJumps = 0;

    [SerializeField] Image damageImage;
    public float health = 60;
    public float maxHealth = 100;
    public float playerEnergy = 100;
    public float maxEnergy;

    public int loadedAmmoGun = 0;
    public int loadedAmmoSMG = 0;
    public int maxAmmoGun = 50;
    public int maxAmmoSMG = 100;
    private float damageAlphaColor = 0;
    private float timer = 20f;
    public Vector3 position;
    public Quaternion rotation;
    public GameObject Player;

    public float speedrunTimer = 0;

    private CharacterController playerCc;

    //health
    public bool hasDiedYet = false;
    public int howManyDeaths = 0;

    private Image healthBar;
    private Image healthBar2;
    private TextMeshProUGUI playerHealthText;
    [SerializeField] Volume volume;
    [SerializeField] Vignette vignette;

    [SerializeField] SteamIntegration si;
    [SerializeField] Map map;
    [SerializeField] PauseMenu pm;
    [SerializeField] SMG smg;
    [SerializeField] Gun gun;


    [SerializeField] GameObject TaskText;
    [SerializeField] GameObject Canvas;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject DeathScreen;

    //Items
    private GameObject pressE;
    private TextMeshProUGUI pressEText;
    [SerializeField] Transform Cam;
    

    //Ammo
    [SerializeField] Gun gunScript;
    [SerializeField] SMG smgScript;

    //Healing
    public float healTimer;
    public bool anyAttacking;

    [SerializeField] Boss_new boss;

    //Audio
    [SerializeField] AudioSource itemPickup;

    public bool isDead = false;

    void Start()
    {
        maxEnergy = playerEnergy;
        Tasks();
        playerCc = Player.GetComponent<CharacterController>();

        healthBar = GameObject.Find("healthBar").GetComponent<Image>();
        healthBar2 = GameObject.Find("healthBar2").GetComponent<Image>();
        playerHealthText = GameObject.Find("playerHealthText").GetComponent<TextMeshProUGUI>();

        pressE = GameObject.Find("PressE");
        pressEText = GameObject.Find("PressEText").GetComponent<TextMeshProUGUI>();

        
        volume.profile.TryGet(out vignette);     
        damageImage.GetComponent<CanvasRenderer>().SetAlpha(0);
        DeathScreen.SetActive(false);
        if(PlayerPrefs.GetInt("laden") == 1)
        {
            LoadPlayer();
            Time.timeScale = 1f;
        }
        SavePlayer();
        Time.timeScale = 1f;
    }

    void Update()
    {
        //Debug.Log(Time.timeScale);
        HealthBar();
        ItemPickup();
        CheckHealing();
        Autosave();
        CrowbarAchievement();
        DeathAchievement();
        //PressEnter();


        if (Input.GetKeyDown(KeyCode.P)){
            TakeDamage(10, 3, transform.position, 0);
        }

        speedrunTimer = speedrunTimer+Time.deltaTime;

        damageAlphaColor -= Time.deltaTime * 2;
        bool ip;
        ip = PauseMenu.isPaused;

        if (!map.mapOpen && !ip) 
        {
            
            Time.timeScale = 1f;
        }
        
    }

    void DeathAchievement()
    {
        if(hasDiedYet)
        {
            if (!si.IsThisAchievementUnlocked("ACH_DIE"))
            {
                si.UnlockAchievements("ACH_DIE");
            }
        }
        if(howManyDeaths>=10)
        {
            if (!si.IsThisAchievementUnlocked("ACH_DIE_10"))
            {
                si.UnlockAchievements("ACH_DIE_10");
            }
        } 
    }

    void CrowbarAchievement()
    {
        if (sandcrab && enemymelee && enemyrange && scavengermelee && scavengerrange)
        {
            if(!si.IsThisAchievementUnlocked("ACH_MELEE"))
            {
                si.UnlockAchievements("ACH_MELEE");
            }
        }
    }

    private void CheckHealing()
    {
        if (healTimer < Time.time)
        {
            health += 1;
            healTimer = Time.time + 0.4f;
        }
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void HealthBar()
    {
        healthBar.fillAmount = Mathf.Clamp(health/maxHealth,0,1);

        if(healthBar2.fillAmount > health/maxHealth)
        {
            healthBar2.fillAmount -= 0.001f;
        }else if(healthBar2.fillAmount < health/maxHealth)
        {
            healthBar2.fillAmount = health/maxHealth;
        }

        playerHealthText.text = health + "/" + maxHealth;
    }

    public void TakeDamage(int amount, float speedReduction, Vector3 enemy, float knockbackForce)
    {
        StartCoroutine(Vignete());

        healTimer = Time.time + 5;
        health -= amount;

        playerMovement.ReduceSpeed(speedReduction, 1);
        playerMovement.Knockback(enemy, knockbackForce);

        if(health <= 0 && !isDead)
        {
            Die();
        }
    }

    IEnumerator Vignete()
    {
        for(float i = 0; i < 0.4f; i += 0.1f)
        {
            vignette.intensity.value = i;
            damageImage.GetComponent<CanvasRenderer>().SetAlpha(i);
            yield return new WaitForSeconds(0.01f);
        }
        for(float i = 0.4f; i > 0; i -= 0.03f)
        {
            damageImage.GetComponent<CanvasRenderer>().SetAlpha(i);
            vignette.intensity.value = i;
            yield return new WaitForSeconds(0.1f);
        }
        vignette.intensity.value = 0;

        
        yield return null;
    }

    private void ItemPickup()
    {        
        RaycastHit hit;
        Transform tempTrans = null;

        if(Physics.Raycast(Cam.position, Cam.forward, out hit, 2))
        {
            tempTrans = hit.transform;
        }else
        {
            foreach(Collider c in Physics.OverlapSphere(Cam.position, 0.5f)){
                if(c.transform.CompareTag("Item"))
                {
                    tempTrans = c.transform;
                    break;
                }
            }
        }
        
        if(tempTrans != null && tempTrans.CompareTag("Item"))
        {
            pressE.SetActive(true);
            pressEText.text = "um " + tempTrans.name + " aufzuheben";

            if (tempTrans.name[..4] == "Herb")
            {
                if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
                {
                    pressEText.text = "um Pflanze aufzuheben";
                }
                else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
                {
                    pressEText.text = "to pickup Plant";
                }
                
            
            }
            else if (tempTrans.name[..5] == "Gnome")
            {
                if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
                {
                    pressEText.text = "um Chompski aufzuheben";
                }
                else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
                {
                    pressEText.text = "to pick up Chompski";
                }

            }


            else if(tempTrans.name[..7] == "LeiterV")
            {
                if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
                {
                    pressEText.text = "um Leiter hochzuklettern.";
                }
                else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
                {
                    pressEText.text = "to climb up Ladder";
                }
                
            }
            else if(tempTrans.name[..7] == "LeiterR")
            {
                if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
                {
                    pressEText.text = "um Leiter herunterzuklettern.";
                }
                else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
                {
                    pressEText.text = "to climb down Ladder";
                }

                
            }
            else if (tempTrans.name[..8] == "CollectE")
            {
                if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
                {
                    pressEText.text = "um den Energiekern aufzuheben";
                }
                else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
                {
                    pressEText.text = "to pick up the energy core";
                }

                //pressEText.text = "um Energiekern aufzuheben.";
            }
            else if (tempTrans.name[..8] == "Gun Ammo")
            {
                if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
                {
                    pressEText.text = "um Pistolenmunition aufzuheben";
                }
                else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
                {
                    pressEText.text = "to pick up pistol ammo";
                }

                //pressEText.text = "um Pistolenmunition aufzuheben";
                
            }
            else if (tempTrans.name[..8] == "SMG Ammo")
            {
                if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
                {
                    pressEText.text = "um SMG Munition aufzuheben";
                }
                else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
                {
                    pressEText.text = "to pick up SMG Ammo";
                }

                //pressEText.text = "um SMG Munition aufzuheben";

            }
            else if (tempTrans.name[..12] == "WeaponPickup")
            {
                if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
                {
                    pressEText.text = "um Speichermodul aufzuheben";
                }
                else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
                {
                    pressEText.text = "to pick up Data Shard";
                }

                //pressEText.text = "um Speichermodul aufzuheben";

            }
            else if (tempTrans.name[..13] == "ClearOutpostQ")
            {
                if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
                {
                    pressEText.text = "um Ersatzteilkiste aufzuheben";
                }
                else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
                {
                    pressEText.text = "to pick up Box with Replacement Parts";
                }

                //pressEText.text = "um Werkzeugkoffer aufzuheben";

            }
            else if (tempTrans.name[..13] == "ClearOutpostM")
            {
                if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
                {
                    pressEText.text = "um Hitzeschild aufzuheben";
                }
                else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
                {
                    pressEText.text = "to pick up Heat Shield";
                }

                //pressEText.text = "um Hitzeschild aufzuheben";

            }



            if (Input.GetKeyDown(KeyCode.E))
            {
                itemPickup.Play();

                if (tempTrans.name[..4] == "Herb")
                {
                    if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
                    {
                        pressEText.text = "um Pflanze aufzuheben";
                    }
                    else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
                    {
                        pressEText.text = "to pickup Plant";
                    }
                    Destroy(tempTrans.gameObject);
                    GameEventsManager.instance.miscEvents.CoinCollected();
                }
                else if (tempTrans.name[..5] == "Gnome")
                {
                    if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
                    {
                        pressEText.text = "um Chompski aufzuheben";
                    }
                    else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
                    {
                        pressEText.text = "to pick up Chompski";
                    }
                    if (!si.IsThisAchievementUnlocked("ACH_GNOME"))
                    {
                        si.UnlockAchievements("ACH_GNOME");
                    }
                    Destroy(tempTrans.gameObject);  

                }
                else if(tempTrans.name[..7] == "LeiterV")
                {
                    if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
                    {
                        pressEText.text = "um Leiter hochzuklettern.";
                    }
                    else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
                    {
                        pressEText.text = "to climb up Ladder";
                    }
                    Player.GetComponent<PlayerMovement>().ClimbLatter(tempTrans.parent.GetChild(1).transform, tempTrans.parent.GetChild(2).transform, tempTrans.parent.GetChild(3).transform);
                }
                else if(tempTrans.name[..7] == "LeiterR")
                {
                    if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
                    {
                        pressEText.text = "um Leiter herunterzuklettern.";
                    }
                    else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
                    {
                        pressEText.text = "to climb down Ladder";
                    }
                    Player.GetComponent<PlayerMovement>().ClimbLatter(tempTrans.parent.GetChild(2).transform, tempTrans.parent.GetChild(1).transform, tempTrans.parent.GetChild(0).transform);
                }
                else if (tempTrans.name[..8] == "CollectE")
                {
                    if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
                    {
                        pressEText.text = "um den Energiekern aufzuheben";
                    }
                    else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
                    {
                        pressEText.text = "to pick up the energy core";
                    }
                    GameEventsManager.instance.miscEvents.ItemPickedUp();
                    Destroy(tempTrans.gameObject);
                }
                else if(tempTrans.name[..8] == "Gun Ammo")
                {
                    if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
                    {
                        pressEText.text = "um Pistolenmunition aufzuheben";
                    }
                    else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
                    {
                        pressEText.text = "to pick up pistol ammo";
                    }
                    gunScript.availableAmmo += Int32.Parse(tempTrans.name.Substring(9, 2));
                    Destroy(tempTrans.gameObject);
                }

                else if(tempTrans.name[..8] == "SMG Ammo")
                {
                    if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
                    {
                        pressEText.text = "um SMG Munition aufzuheben";
                    }
                    else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
                    {
                        pressEText.text = "to pick up SMG Ammo";
                    }
                    smgScript.availableAmmo += Int32.Parse(tempTrans.name.Substring(9, 2));
                    Destroy(tempTrans.gameObject);
                }
                else if(tempTrans.name[..12] == "WeaponPickup")
                {
                    if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
                    {
                        pressEText.text = "um Speichermodul aufzuheben";
                    }
                    else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
                    {
                        pressEText.text = "to pick up Data Shard";
                    }
                    GameEventsManager.instance.miscEvents.ItemPickedUp();
                    Destroy(tempTrans.gameObject);
                }
                else if (tempTrans.name[..13] == "ClearOutpostQ")
                {
                    if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
                    {
                        pressEText.text = "um Ersatzteilkiste aufzuheben";
                    }
                    else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
                    {
                        pressEText.text = "to pick up Box with Replacement Parts";
                    }
                    GameEventsManager.instance.miscEvents.ItemPickedUp();
                    Destroy(tempTrans.gameObject);
                }
                else if (tempTrans.name[..13] == "ClearOutpostM")
                {
                    if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
                    {
                        pressEText.text = "um Hitzeschild aufzuheben";
                    }
                    else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
                    {
                        pressEText.text = "to pick up Heat Shield";
                    }
                    GameEventsManager.instance.miscEvents.ItemPickedUp();
                    Destroy(tempTrans.gameObject);
                }



            }
        }else
        {
            pressE.SetActive(false);
        }
    }

    /*private void PressEnter()
    {
        RaycastHit hit;

        if (Physics.Raycast(Cam.position, Cam.forward, out hit, 2) && hit.transform.CompareTag("Deposit"))
        {
            pressEnter.SetActive(true);

        }
        else
        {
            pressEnter.SetActive(false);
        }
    }*/

    private void Tasks()
    {
        
    }

    public void Die()
    {
        isDead = true;
        hasDiedYet = true;
        howManyDeaths++;
        Debug.Log(howManyDeaths);

        Time.timeScale = 0f;

        boss.ResetBoss();

        DeathScreen.SetActive(true);
    }

    public void Autosave()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        anyAttacking = false;
        foreach (Enemy enemy in enemies)
        {
            if (enemy.continueCharge)
            {
                anyAttacking = true;
            }
        }

        if (!anyAttacking)
        {
            timer -= Time.deltaTime;
            if (timer < 0f)
            {
                SaveSystem.SavePlayer(this);
                Debug.Log("Autosave happened");
                timer = 120f;
            }
        }
    }

    public void SavePlayer ()
    {
        if(!anyAttacking)
        {
            totalJumps = playerMovement.totalJumps;
            loadedAmmoSMG = smg.loadedAmmo;
            maxAmmoSMG = smg.availableAmmo;
            loadedAmmoGun = gun.loadedAmmo;
            maxAmmoGun = gun.availableAmmo;
            SaveSystem.SavePlayer(this);
        }
    }

    public void LoadPlayer()
    {
        isDead = false;
        Debug.Log("hier beginnt load");

        DeathScreen.SetActive(false);
        PlayerData data = SaveSystem.LoadPlayer();
        health = data.health;
        maxHealth = data.maxHealth;
        playerEnergy = data.energy;
        maxEnergy = data.maxEnergy;

        totalJumps = data.totalJumps;
        playerMovement.totalJumps = totalJumps;
        //hier noch ein kurzer fix


        loadedAmmoSMG = data.loadedAmmoSMG;
        maxAmmoSMG = data.maxAmmoSMG;
        Debug.Log(maxAmmoSMG);
        maxAmmoGun = data.maxAmmoGun;
        Debug.Log(maxAmmoGun);
        loadedAmmoGun = data.loadedAmmoGun;

        smg.loadedAmmo = loadedAmmoSMG;
        smg.availableAmmo = maxAmmoSMG;
        gun.availableAmmo = maxAmmoGun;
        gun.loadedAmmo = loadedAmmoGun;

        speedrunTimer = data.speedrunTimer; 
        if (data.hasDiedYet)
        {
            hasDiedYet = true;
        }

        if (data.howManyDeaths > 0)
        {
            howManyDeaths++;
        }

        //das crowbar zeug

        sandcrab = data.sandcrab;
        enemyrange = data.enemyrange;
        enemymelee  = data.enemymelee;
        scavengerrange = data.scavengerrange;
        scavengermelee = data.scavengermelee;

        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        rotation.x = data.rotation[0];
        rotation.y = data.rotation[1];
        rotation.z = data.rotation[2];
        rotation.w = data.rotation[3];
        
        Debug.Log(position + "gespeicherter vector");

        playerCc.enabled = false;
        transform.position = position;
        transform.rotation = rotation;
        Debug.Log(transform.position + "aktuelle position");
        playerCc.enabled = true;

        Time.timeScale = 1f;

        Debug.Log("hier endet load");

    }

    public void TitleScreen()
    {
        isDead = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScreen");
    }

    public void Retry() 
    { 
        Time.timeScale = 1f;
        LoadPlayer();
    }
    
}
   