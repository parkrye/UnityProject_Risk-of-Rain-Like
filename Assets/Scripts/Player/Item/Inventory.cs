using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    PlayerDataModel playerDataModel;

    Dictionary<ItemData, int> inventory;

    void Awake()
    {
        playerDataModel = GetComponent<PlayerDataModel>();
        inventory = new Dictionary<ItemData, int>();
    }

    public void AddItem(ItemData item)
    {
        if (inventory.ContainsKey(item))
        {
            inventory[item]++;
            item.GetFirstEffect();
        }
        else
        {
            inventory.Add(item, 1);
            item.GetNextEffect();
        }
    }
}
