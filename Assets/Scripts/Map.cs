using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public GameObject Player;
    public GameObject SpawnPointManager;
    public GameObject Canvas;
    public bool canMapOpen = false;
    public bool toggle = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            toggle = !toggle;
        }
        
        if (toggle && canMapOpen)
        {
            Canvas.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Player.GetComponent<PlayerMovement>().locked = true;
        }
        else
        {
            Canvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Player.GetComponent<PlayerMovement>().locked = false;
        }
        
    }


}
