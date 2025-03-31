using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public GameObject DialogeCanvas;

    public GameObject Player;
    public GameObject SpawnPointManager;
    public GameObject StandardCanvas;
    public GameObject MapCanvas;
    public GameObject arrow;
    public GameObject MapCamera;
    public GameObject PlayerCamera;
    public GameObject InvManager;
    public GameObject Disabler1;
    public GameObject Disabler2;
    public bool canMapOpen = true;
    public bool toggle = false;
    public bool mapOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        MapCanvas.SetActive(false);
        MapCamera.SetActive(false);
        arrow.SetActive(false);
        Disabler1.SetActive(false);
        Disabler2.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PauseMenu.isPaused);
        if (!PauseMenu.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.M)) //&& canMapOpen
            {
                //Debug.Log(toggle + "toggle");
                toggle = !toggle;
            }

            if (toggle) //&& canMapOpen)
            {
                ActivateCanvas();
            }
            else
            {
                DialogeCanvas.SetActive(true);
                MapCanvas.SetActive(false);
                StandardCanvas.SetActive(true);
                MapCamera.SetActive(false);
                PlayerCamera.SetActive(true);
                arrow.SetActive(false);
                mapOpen = false;


                if (!toggle)
                {
                    Time.timeScale = 1f;
                    //Debug.Log(canMapOpen + " " + toggle);
                }
            }
        }
        

    }

    public void ActivateCanvas()
    {
        /*if (InvManager.GetComponent<InventoryManager>().invactive) 
        {
            InvManager.GetComponent<InventoryManager>().Inventory.SetActive(false);
            InvManager.GetComponent<InventoryManager>().invactive = false;
            Debug.Log("invactive false map");
        }*/
        StandardCanvas.SetActive(false);
        MapCanvas.SetActive(true);
        DialogeCanvas.SetActive(false);
        MapCamera.SetActive(true);
        arrow.SetActive(true);
        mapOpen=true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0f;
        
    }
    


}
