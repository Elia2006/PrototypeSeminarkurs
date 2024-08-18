using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI playerEnergyText;
    [SerializeField] Image damageImage;
    public float playerHealth = 100;
    public float maxHealth;
    public float playerEnergy = 100;
    public float maxEnergy;
    private float damageAlphaColor = 0;
    public Vector3 position;
    public GameObject Player;
    private CharacterController playerCc;
    [SerializeField] Image healthBar;
    [SerializeField] Image energyBar;
    [SerializeField] GameObject TaskText;
    [SerializeField] GameObject Canvas;
    void Start()
    {
        maxHealth = playerHealth;
        maxEnergy = playerEnergy;
        Tasks();
        playerCc = Player.GetComponent<CharacterController>();
    }

    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(playerHealth/maxHealth,0,1);
        energyBar.fillAmount = Mathf.Clamp(playerEnergy / maxEnergy, 0, 1);
        playerHealthText.text = playerHealth + "/" + maxHealth;
        playerEnergyText.text = playerEnergy + "/" + maxEnergy;

        Color newColor = new Color(1, 1, 1, damageAlphaColor);
        damageImage.color = newColor;

        damageAlphaColor -= Time.deltaTime * 2;
    }

    public void TakeDamage(int amount)
    {
        playerHealth -= amount;
        damageAlphaColor = 0.8f;

        if(playerHealth <= 0)
        {
            //Die();
        }
    }

    private void Tasks()
    {
        
    }

    public void Die()
    {
        SceneManager.LoadScene("StartMenu");
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
        Debug.Log(position + "gespeicherter vector");

        playerCc.enabled = false;
        transform.position = position;
        Debug.Log(transform.position + "aktuelle position");
        playerCc.enabled = true;

    }
}
   