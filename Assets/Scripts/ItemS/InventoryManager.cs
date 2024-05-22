using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<ItemScriptableObject> Items = new List<ItemScriptableObject>();

    public Transform ItemContent;
    public GameObject InventoryItem;
    public GameObject Inventory;
    public GameObject Player;

    public Toggle EnableRemove;

    public InventoryItemController[] InventoryItems;

    public bool invactive = false;
    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && invactive == false)
        {
            Player.GetComponent<PlayerMovement>().locked = true;
            Debug.Log(Player.GetComponent<PlayerMovement>().locked + "invmanager");
            Inventory.SetActive(true);
            ListItems();
            invactive = true;
        }  

        else if (Input.GetKeyDown(KeyCode.Tab) && invactive == true)
        {
            Player.GetComponent<PlayerMovement>().locked = false;
            Debug.Log(Player.GetComponent<PlayerMovement>().locked + "invmanager");
            Inventory.SetActive(false);
            invactive = false;
        }
    }


    public void Add (ItemScriptableObject item)
    {
        Items.Add (item);
    }

    public void Remove (ItemScriptableObject item)
    {
        Items.Remove (item);
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("Image").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            if (EnableRemove.isOn)
            {
               removeButton.gameObject.SetActive (true);
            }
        }

        SetInventoryItems();
    }

    public void EnableItemsRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }

        }
        else
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }
    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for(int i = 0; i<Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }
    }
}
