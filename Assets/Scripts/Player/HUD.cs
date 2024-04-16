using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI playerHealthText;
    [SerializeField] Image damageImage;
    public float playerHealth = 100;
    public float maxHealth;
    private float damageAlphaColor = 0;
    public Image healthBar;
    void Start()
    {
        maxHealth = playerHealth;
    }

    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(playerHealth/maxHealth,0,1);
        Debug.Log(healthBar.fillAmount);
        playerHealthText.text = playerHealth + "/100";

        Color newColor = new Color(1, 1, 1, damageAlphaColor);
        damageImage.color = newColor;

        damageAlphaColor -= Time.deltaTime * 2;
    }

    public void TakeDamage(int amount)
    {
        if(playerHealth <= 0)
        {
            Die();
        }

        playerHealth -= amount;
        damageAlphaColor = 0.8f;
    }

    public void Die()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
   