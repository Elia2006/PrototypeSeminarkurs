using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] Transform Player;
    private Enemy[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindObjectsOfType<Enemy>();

        StartCoroutine(CheckIfLoaded());
    }

    // Update is called once per frame
    IEnumerator CheckIfLoaded()
    {
        while (true)
        {
            foreach(Enemy enemy in enemies)
            {
                if(Vector3.Distance(Player.position, enemy.transform.position) < 100)
                {
                    enemy.gameObject.SetActive(true);
                }else
                {
                    enemy.gameObject.SetActive(false);
                }
            }

            yield return new WaitForSeconds(1);
        }
    }
}
