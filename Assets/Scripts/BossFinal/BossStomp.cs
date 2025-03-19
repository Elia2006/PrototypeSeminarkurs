using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStomp : MonoBehaviour
{
    public int lifeCicle;
    private HUD playerHUD;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Main());
        playerHUD = GameObject.Find("Player").GetComponent<HUD>();
    }

    IEnumerator Main()
    {
        yield return new WaitForSeconds(0.2f);

        if(lifeCicle >= 0)
        {
            GameObject nextCicle = Instantiate(gameObject, transform.position + transform.forward * 2, transform.rotation);
            nextCicle.GetComponent<BossStomp>().lifeCicle = lifeCicle - 1;
        }

        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);

        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameObject[] bossStomps = GameObject.FindGameObjectsWithTag("BossStomp");

            foreach(GameObject bossStomp in bossStomps)
            {
                bossStomp.GetComponent<Collider>().enabled = false;
            }

            playerHUD.TakeDamage(20, 2, transform.position, 0.1f);
            Destroy(gameObject);
        }
    }
}
