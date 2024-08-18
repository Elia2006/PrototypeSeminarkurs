using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float health;
    public float maxHealth;
    public float energy;
    public float maxEnergy;
    public float[] position;

    public PlayerData (HUD player)
    {
        health = player.playerHealth;
        maxHealth = player.maxHealth;
        energy = player.playerEnergy;
        maxEnergy = player.maxEnergy;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
