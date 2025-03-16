using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerTarget : MonoBehaviour
{
    [SerializeField] Transform BossPos;
    private Transform Player;
    [SerializeField] Transform flamethrowerEnd;
    [SerializeField] Transform flamethrowerDef;
    [SerializeField] GameObject fire;

    private bool coroutineRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!coroutineRunning)
        {
            transform.position = Vector3.Lerp(transform.position, Player.position, 0.1f);
        }
    }

    public void StartFlames()
    {
        StartCoroutine(CStartFlames());
    }

    IEnumerator CStartFlames()
    {
        coroutineRunning = true;

        for(int i = 0; i < 40; i++)
        {
            transform.position = Vector3.Lerp(transform.position, flamethrowerDef.position, 0.1f);
            yield return new WaitForSeconds(0.05f);
        }
        transform.LookAt(Player.transform.position);
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y, 0));

        for(int i = 0; i < 50; i++)
        {
            transform.position += transform.forward * 1f; 

            //float rand = 1 / Vector3.Distance(transform.position, Player.transform.position) * 50;
            //flamethrowerEnd.rotation *= Quaternion.Euler(Random.Range(-rand, rand), Random.Range(-rand, rand), 0);

            Instantiate(fire, flamethrowerEnd.position, flamethrowerEnd.rotation);  

            yield return new WaitForSeconds(0.01f);
        }

        coroutineRunning = false;

        yield return null;
    }
}
