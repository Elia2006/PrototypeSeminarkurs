using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_new : MonoBehaviour
{
    [SerializeField] Transform Player;

    //Flamethrower
    [SerializeField] GameObject fire;
    [SerializeField] Transform flamethrowerEnd;
    [SerializeField] GameObject Missile;
    private float flameCooldown;

    // Start is called before the first frame update
    void Start()
    {
                StartCoroutine(Missiles());

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

    IEnumerator Missiles()
    {
        Vector3 offsetPosition = new Vector3(2.28999996f,7.32800007f,-2.14899993f);
        Quaternion offsetQuaternion = new Quaternion(-0.0732644945f,-0.00171960483f,-0.103516184f,0.991924286f);
        
        for(int x = 0; x < 4; x++)
        {
            for(int y = 0; y < 3; y++)
            {
                GameObject temp = Instantiate(Missile, transform.position + offsetPosition, transform.rotation * offsetQuaternion, transform);
                temp.transform.localPosition += -temp.transform.right * (0.23f * x);
                temp.transform.localPosition += -temp.transform.up * (0.23f * y);
                StartCoroutine(MoveMissiles(temp.transform, 1 + x + y * 4));
            }
        }

        yield return null;
    }

    IEnumerator MoveMissiles(Transform missile, float time)
    {
        for(int i = 0; i < 20;  i++)
        {
            missile.position += missile.forward * 0.04f;
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(time);

        missile.transform.parent = null;

        missile.GetComponent<Missile_new>().enabled = true;

        yield return null;
    }
}