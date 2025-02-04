using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_new : MonoBehaviour
{
    [SerializeField] Transform Player;

    //Flamethrower
    [SerializeField] GameObject fire;
    [SerializeField] Transform flamethrowerEnd;
    private float flameCooldown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {/*
        transform.LookAt(Player.position);
        transform.localRotation = Quaternion.Euler(-90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);*/

        var lookRotation = Quaternion.LookRotation(Player.transform.position - transform.position, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.01f);

        transform.localRotation = Quaternion.Euler(-90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        Flamethrower();
    }

    private void Flamethrower()
    {
        if(flameCooldown < Time.time)
        {
            Instantiate(fire, flamethrowerEnd.position, flamethrowerEnd.rotation);  

            flameCooldown = Time.time + 0.4f;
        }
    }
}