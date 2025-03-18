using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] Transform Player;
    private Enemy[] enemies;
    private GameObject[] items;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindObjectsOfType<Enemy>();
        items = GameObject.FindGameObjectsWithTag("Item");
        StartCoroutine(CheckIfLoaded());
    }

    // Update is called once per frame
    IEnumerator CheckIfLoaded()
    {
        while (true)
        {
            
            foreach(Enemy enemy in enemies)
            {
                if(enemy != null)
                {
                    if(Vector3.Distance(Player.position, enemy.transform.position) < 100)
                    {
                        enemy.gameObject.SetActive(true);
                    }else
                    {
                        enemy.gameObject.SetActive(false);
                    }
                }
            }
            foreach(GameObject item in items)
            {
                if(item != null)
                {
                    if (Vector3.Distance(Player.position, item.transform.position) < 100)
                    {
                        item.SetActive(true);
                    }
                    else
                    {
                        item.SetActive(false);
                    }
                }
                
            }
            Debug.Log("hello");
            yield return new WaitForSeconds(1);
        }
    }
}
