using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLocker : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Player.GetComponent<PlayerMovement>().locked = true;
        Debug.Log(Player.GetComponent<PlayerMovement>().locked + "gamelocker");
    }
}
