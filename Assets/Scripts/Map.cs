using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public GameObject Player;
    public GameObject SpawnPointManager;
    public GameObject Canvas;
    public bool canMapOpen = false;
    public bool toggle = false;
    public bool mapOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && canMapOpen)
        {
            toggle = !toggle;
        }

        if (toggle && canMapOpen)
        {
            ActivateCanvas();
        }
        else 
        {
            Canvas.SetActive(false);
            mapOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            if (canMapOpen && !toggle) 
            {
                Player.GetComponent<PlayerMovement>().locked = false;
                Debug.Log(Player.GetComponent<PlayerMovement>().locked + "map");
            }
        }

    }

    public void ActivateCanvas()
    {
        Canvas.SetActive(true);
        mapOpen=true;
        Cursor.lockState = CursorLockMode.Confined;
        Player.GetComponent<PlayerMovement>().locked = true;
        Debug.Log(Player.GetComponent<PlayerMovement>().locked + "map");
    }


}
