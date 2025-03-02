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
    void Start()
    {
        maxHealth = playerHealth;
        maxEnergy = playerEnergy;
        Tasks();
        playerCc = Player.GetComponent<CharacterController>();

        healthBar = GameObject.Find("healthBar").GetComponent<Image>();
        healthBar2 = GameObject.Find("healthBar2").GetComponent<Image>();
        playerHealthText = GameObject.Find("playerHealthText").GetComponent<TextMeshProUGUI>();

        volume.profile.TryGet(out vignette);     

        StartCoroutine(Vignete()); 
    }

    void Update()
    {

        HealthBar();

        Color newColor = new Color(1, 1, 1, damageAlphaColor);
        //damageImage.color = newColor;

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

    public void TakeDamage(int amount, float speedReduction, Transform enemy, float knockbackForce)
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

    private void Tasks()
    {
        
    }

    public void Die()
    {
        Time.timeScale = 0f;
        DeathScreen.SetActive(true);
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
   