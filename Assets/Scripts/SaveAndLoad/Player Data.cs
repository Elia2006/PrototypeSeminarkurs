using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float health;
    public float maxHealth;
    public float energy;
    public float maxEnergy;
    public float speedrunTimer;
    public bool hasDiedYet;
    public int howManyDeaths;
    public float[] position;
    public float[] rotation;

    public float totalJumps;

    public int loadedAmmoGun;
    public int loadedAmmoSMG;
    public int maxAmmoGun;
    public int maxAmmoSMG;

    public bool sandcrab;
    public bool enemyrange;
    public bool enemymelee;
    public bool scavengerrange;
    public bool scavengermelee;

    public PlayerData (HUD player)
    {
        health = player.health;
        maxHealth = player.maxHealth;
        energy = player.playerEnergy;
        maxEnergy = player.maxEnergy;

        totalJumps = player.totalJumps;

        loadedAmmoGun = player.loadedAmmoGun;
        loadedAmmoSMG = player.loadedAmmoSMG;
        maxAmmoSMG = player.maxAmmoSMG;
        maxAmmoGun = player.maxAmmoGun;

        speedrunTimer = player.speedrunTimer;
        hasDiedYet = player.hasDiedYet;
        howManyDeaths = player.howManyDeaths;

        sandcrab = player.sandcrab;
        enemyrange = player.enemyrange;
        enemymelee = player.enemymelee;
        scavengerrange = player.scavengerrange;
        scavengermelee = player.scavengermelee;



        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        rotation = new float[4];
        rotation[0] = player.transform.rotation.x;
        rotation[1] = player.transform.rotation.y;
        rotation[2] = player.transform.rotation.z;
        rotation[3] = player.transform.rotation.w;
    }
}
