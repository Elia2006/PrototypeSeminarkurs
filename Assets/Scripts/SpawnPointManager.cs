using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public GameObject[] SpawnPoints;
    public GameObject Player;
    private CharacterController playerCc;

    // Start is called before the first frame update
    void Start()
    {
        playerCc = Player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Teleport (int teleporterindex)
    {
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            if (SpawnPoints[i].transform.GetChild(0).GetComponent<SpawnPoint>().teleport)
            {
                //Der Charakter Controller findets gar nicht gut teleportiert zu werden, daher muss er kurz abgestellt werden

                playerCc.enabled = false;
                Player.transform.position = SpawnPoints[teleporterindex].transform.position + new Vector3(0, 1.2f, 0);
                playerCc.enabled = true;
                SpawnPoints[i].transform.GetChild(0).GetComponent<SpawnPoint>().teleport = false;
            }
        }
    }
}
