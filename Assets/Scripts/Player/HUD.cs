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
    public int playerHealth = 100;
    private float damageAlphaColor = 0;
    void Start()
    {

    }

    void Update()
    {
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
   