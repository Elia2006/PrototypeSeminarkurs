using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public GameObject Canvas;
    public bool mapOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Canvas.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Canvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


}
