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
    {
        TurnTowardsPlayer();
        GoTowardsPlayer();

        //Flamethrower();
    }

    private void TurnTowardsPlayer()
    {
        var lookRotation = Quaternion.LookRotation(Player.transform.position - transform.position, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.01f);

        transform.localRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private void GoTowardsPlayer()
    {
        if(Vector3.Distance(transform.position, Player.position) > 20)
        {
            transform.position += transform.forward * Time.deltaTime;
        }
    }

    private void Flamethrower()
    {
        if(flameCooldown < Time.time)
        {
            

            flamethrowerEnd.LookAt(Player.transform);

            float rand = 1 / Vector3.Distance(transform.position, Player.position) * 50;
            flamethrowerEnd.rotation *= Quaternion.Euler(Random.Range(-rand, rand), Random.Range(-rand, rand), 0);

            Instantiate(fire, flamethrowerEnd.position, flamethrowerEnd.rotation);  

            flameCooldown = Time.time + 0.7f;
        }
    }
}