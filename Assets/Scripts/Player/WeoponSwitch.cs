using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeoponSwitch : MonoBehaviour
{
    [SerializeField] GameObject Weapon1;
    [SerializeField] GameObject Weapon2;
    // Start is called before the first frame update
    void Start()
    {
        Weapon1.SetActive(true);
        Weapon2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Weapon1.GetComponent<Animator>().Stop();
            Weapon1.SetActive(true);
            Weapon2.SetActive(false);
        } else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Weapon1.SetActive(false);
            Weapon2.SetActive(true);
        }


    }
}
