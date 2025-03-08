using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class HUD : MonoBehaviour
{

    [SerializeField] Image damageImage;
    public float playerHealth = 100;
    public float maxHealth;
    public float playerEnergy = 100;
    public float maxEnergy;
    private float damageAlphaColor = 0;
    public Vector3 position;
    public Quaternion rotation;
    public GameObject Player;

    private CharacterController playerCc;

    //health
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
    private GameObject pressEnter;

    //Ammo
    [SerializeField] Gun gunScript;
    [SerializeField] SMG smgScript;

    //Audio
    [SerializeField] AudioSource itemPickup;

    void Start()
    {
        maxHealth = playerHealth;
        maxEnergy = playerEnergy;
        Tasks();
        playerCc = Player.GetComponent<CharacterController>();

        healthBar = GameObject.Find("healthBar").GetComponent<Image>();
        healthBar2 = GameObject.Find("healthBar2").GetComponent<Image>();
        playerHealthText = GameObject.Find("playerHealthText").GetComponent<TextMeshProUGUI>();

        pressE = GameObject.Find("PressE");
        pressEText = GameObject.Find("PressEText").GetComponent<TextMeshProUGUI>();

        pressEnter = GameObject.Find("PressEnter");
        volume.profile.TryGet(out vignette);     
        damageImage.GetComponent<CanvasRenderer>().SetAlpha(0);
    }

    void Update()
    {

        HealthBar();
        ItemPickup();
        //PressEnter();


        damageAlphaColor -= Time.deltaTime * 2;
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

        if(Physics.Raycast(Cam.position, Cam.forward, out hit, 2) && hit.transform.CompareTag("Item"))
        {
            pressE.SetActive(true);
            pressEText.text = "Press E to Pickup " + hit.transform.name;

            if(Input.GetKeyDown(KeyCode.E))
            {
                itemPickup.Play();

                if(hit.transform.name[..8] == "Gun Ammo")
                {
                    gunScript.availableAmmo += Int32.Parse(hit.transform.name.Substring(9, 2));
                    Destroy(hit.transform.gameObject);
                }

                if(hit.transform.name[..8] == "SMG Ammo")
                {
                    smgScript.availableAmmo += Int32.Parse(hit.transform.name.Substring(9, 2));
                    Destroy(hit.transform.gameObject);
                }

                if (hit.transform.name[..8] == "Herb")
                {

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
        Time.timeScale = 0f;
        //DeathScreen.SetActive(true);
    }

    public void SavePlayer ()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        playerHealth = data.health;
        maxHealth = data.maxHealth;
        playerEnergy = data.energy;
        maxEnergy = data.maxEnergy;

        
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
   