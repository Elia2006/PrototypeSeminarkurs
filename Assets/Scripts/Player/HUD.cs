using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    [SerializeField] Image healthBar;
    [SerializeField] GameObject TaskText;
    [SerializeField] GameObject Canvas;
    void Start()
    {
        maxHealth = playerHealth;
        Tasks();
    }

    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(playerHealth/maxHealth,0,1);
        playerHealthText.text = playerHealth + "/100";

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
}
   