using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class HUD : MonoBehaviour
{

    [SerializeField] Image damageImage;
    public float playerHealth = 60;
    public float maxHealth = 100;
    public float playerEnergy = 100;
    public float maxEnergy;
    private float damageAlphaColor = 0;
    private float timer = 20f;
    public Vector3 position;
    public Quaternion rotation;
    public GameObject Player;

    public float speedrunTimer = 0;

    private CharacterController playerCc;

    //health
    public bool hasDiedYet = false;

    private Image healthBar;
    private Image healthBar2;
    private TextMeshProUGUI playerHealthText;
    [SerializeField] Volume volume;
    [SerializeField] Vignette vignette;


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
    private Coroutine healing;
    public bool anyAttacking;

    [SerializeField] Boss_new boss;

    //Audio
    [SerializeField] AudioSource itemPickup;

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
        }
        SavePlayer();
        
    }

    void Update()
    {
        HealthBar();
        ItemPickup();
        CheckHealing();
        Autosave();
        //PressEnter();

        speedrunTimer = speedrunTimer+Time.deltaTime;

        damageAlphaColor -= Time.deltaTime * 2;
    }
    private void CheckHealing()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        anyAttacking = false;
        foreach(Enemy enemy in enemies)
        {
            if(enemy.continueCharge)
            {
                anyAttacking = true;
            }   
        }
        if(!anyAttacking && healing == null && playerHealth < maxHealth)
        {
            healing = StartCoroutine(Healing());
        }
        else if(healing != null && anyAttacking)
        {
            StopCoroutine(healing);
            healing = null;
        }
    }
    IEnumerator Healing()
    {
        yield return new WaitForSeconds(5);
        while(playerHealth <= maxHealth - 1)
        {
            playerHealth += 1;
            yield return new WaitForSeconds(0.2f);
        }
        playerHealth = maxHealth;
        yield return null;
        
    }
    private void HealthBar()
    {
        healthBar.fillAmount = Mathf.Clamp(playerHealth/maxHealth,0,1);

        if(healthBar2.fillAmount > playerHealth/maxHealth)
        {
            healthBar2.fillAmount -= 0.001f;
        }else if(healthBar2.fillAmount < playerHealth/maxHealth)
        {
            healthBar2.fillAmount = playerHealth/maxHealth;
        }

        playerHealthText.text = playerHealth + "/" + maxHealth;
    }

    public void TakeDamage(int amount, float speedReduction, Vector3 enemy, float knockbackForce)
    {
        StartCoroutine(Vignete());

        playerHealth -= amount;

        playerMovement.ReduceSpeed(speedReduction, 1);
        playerMovement.Knockback(enemy, knockbackForce);

        if(playerHealth <= 0)
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
                pressEText.text = "um Pflanze aufzuheben";
            
            }            
            else if(tempTrans.name[..7] == "LeiterV")
            {
                pressEText.text = "um Leiter hochzuklettern.";
            }
            else if(tempTrans.name[..7] == "LeiterR")
            {
                pressEText.text = "um Leiter runterzuklettern.";
            }
            else if (tempTrans.name[..8] == "CollectE")
            {
                pressEText.text = "um Energiekern aufzuheben.";
            }
            else if (tempTrans.name[..8] == "Gun Ammo")
            {
                pressEText.text = "um Pistolenmunition aufzuheben";
                
            }
            else if (tempTrans.name[..8] == "SMG Ammo")
            {
                pressEText.text = "um SMG Munition aufzuheben";
                
            }
            else if (tempTrans.name[..12] == "WeaponPickup")
            {
                pressEText.text = "um Speichermodul aufzuheben";
                
            }
            else if (tempTrans.name[..13] == "ClearOutpostQ")
            {
                pressEText.text = "um Werkzeugkoffer aufzuheben";
                
            }
            else if (tempTrans.name[..13] == "ClearOutpostM")
            {
                pressEText.text = "um Hitzeschild aufzuheben";
                
            }



            if (Input.GetKeyDown(KeyCode.E))
            {
                itemPickup.Play();

                if (tempTrans.name[..4] == "Herb")
                {
                    pressEText.text = "um Pflanze aufzuheben";
                    Destroy(tempTrans.gameObject);
                    GameEventsManager.instance.miscEvents.CoinCollected();
                }
                else if(tempTrans.name[..7] == "LeiterV")
                {
                    pressEText.text = "um Leiter hochzuklettern.";
                    Player.GetComponent<PlayerMovement>().ClimbLatter(tempTrans.parent.GetChild(1).transform, tempTrans.parent.GetChild(2).transform, tempTrans.parent.GetChild(3).transform);
                }
                else if(tempTrans.name[..7] == "LeiterR")
                {
                    pressEText.text = "um Leiter runterzuklettern.";
                    Player.GetComponent<PlayerMovement>().ClimbLatter(tempTrans.parent.GetChild(2).transform, tempTrans.parent.GetChild(1).transform, tempTrans.parent.GetChild(0).transform);
                }
                else if (tempTrans.name[..8] == "CollectE")
                {
                    pressEText.text = "um Energiekern aufzuheben.";
                    GameEventsManager.instance.miscEvents.ItemPickedUp();
                    Destroy(tempTrans.gameObject);
                }
                else if(tempTrans.name[..8] == "Gun Ammo")
                {
                    pressEText.text = "um Pistolenmunition aufzuheben";
                    gunScript.availableAmmo += Int32.Parse(tempTrans.name.Substring(9, 2));
                    Destroy(tempTrans.gameObject);
                }

                else if(tempTrans.name[..8] == "SMG Ammo")
                {
                    pressEText.text = "um SMG Munition aufzuheben";
                    smgScript.availableAmmo += Int32.Parse(tempTrans.name.Substring(9, 2));
                    Destroy(tempTrans.gameObject);
                }
                else if(tempTrans.name[..12] == "WeaponPickup")
                {
                    pressEText.text = "um Speichermodul aufzuheben";
                    GameEventsManager.instance.miscEvents.ItemPickedUp();
                    Destroy(tempTrans.gameObject);
                }
                else if (tempTrans.name[..13] == "ClearOutpostQ")
                {
                    pressEText.text = "um Werkzeugkoffer aufzuheben";
                    GameEventsManager.instance.miscEvents.ItemPickedUp();
                    Destroy(tempTrans.gameObject);
                }
                else if (tempTrans.name[..13] == "ClearOutpostM")
                {
                    pressEText.text = "um Hitzeschild aufzuheben";
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
        hasDiedYet = true;

        Time.timeScale = 0f;

        boss.ResetBoss();

        DeathScreen.SetActive(true);
    }

    public void Autosave()
    {
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
            SaveSystem.SavePlayer(this);
        }
    }

    public void LoadPlayer()
    {
        DeathScreen.SetActive(false);
        PlayerData data = SaveSystem.LoadPlayer();
        playerHealth = data.health;
        maxHealth = data.maxHealth;
        playerEnergy = data.energy;
        maxEnergy = data.maxEnergy;

        speedrunTimer = data.speedrunTimer; 
        hasDiedYet = data.hasDiedYet;

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

    }

    public void TitleScreen()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScreen");
    }

    public void Retry() 
    { 
        Time.timeScale = 1f;
        LoadPlayer();
    }
    
}
   