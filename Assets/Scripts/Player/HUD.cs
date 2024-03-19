using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI playerHealthText;
    public int playerHealth = 100;
    void Start()
    {

    }

    void Update()
    {
        playerHealthText.text = playerHealth + "/100";
    }

    public void TakeDamage(int amount)
    {
        playerHealth -= amount;
    }

    public void Die()
    {
        //SceneManager.LoadScene(Death);
    }
}
   