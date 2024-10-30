using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public GameObject Player;
    public GameObject SpawnPointManager;
    public GameObject StandardCanvas;
    public GameObject MapCanvas;
    public GameObject arrow;
    public GameObject MapCamera;
    public GameObject PlayerCamera;
    public GameObject InvManager;
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
        if (!PauseMenu.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.M) && canMapOpen)
            {
                toggle = !toggle;
            }

            if (toggle && canMapOpen)
            {
                ActivateCanvas();
            }
            else
            {
                MapCanvas.SetActive(false);
                StandardCanvas.SetActive(true);
                MapCamera.SetActive(false);
                PlayerCamera.SetActive(true);
                //arrow.SetActive(false);
                mapOpen = false;


                if (canMapOpen && !toggle)
                {
                    Time.timeScale = 1f;
                    Debug.Log(canMapOpen + " " + toggle);
                }
            }
        }
        

    }

    public void ActivateCanvas()
    {
        if (InvManager.GetComponent<InventoryManager>().invactive) 
        {
            InvManager.GetComponent<InventoryManager>().Inventory.SetActive(false);
            InvManager.GetComponent<InventoryManager>().invactive = false;
            Debug.Log("invactive false map");
        }
        StandardCanvas.SetActive(false);
        MapCanvas.SetActive(true);
        
        MapCamera.SetActive(true);
        arrow.SetActive(true);
        mapOpen=true;
        Cursor.lockState = CursorLockMode.Confined;
        Debug.Log("openmap");
        Time.timeScale = 0f;
        
    }
    


}
