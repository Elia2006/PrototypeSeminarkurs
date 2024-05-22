using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemScriptableObject item;

    public bool interact = false;
    private Transform Cam;

    // Start is called before the first frame update
    void Start()
    {
        Cam = GameObject.Find("PlayerCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Physics.Raycast(Cam.position, Cam.forward, out hit, 8);

        if(hit.collider == gameObject.GetComponent<Collider>() && Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }
    
    }

    void Pickup()
    {
        InventoryManager.Instance.Add(item);
        Destroy(gameObject);
    }
}
