using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    Dictionary<ItemData, int> inventory;
    public UnityEvent<ItemData, int> ItemEvent;

    void Awake()
    {
        inventory = new Dictionary<ItemData, int>();
    }

    public void AddItem(ItemBase item)
    {
        if (inventory.ContainsKey(item.itemData))
        {
            inventory[item.itemData]++;
            item.GetFirstEffect();
        }
        else
        {
            inventory.Add(item.itemData, 1);
            item.GetNextEffect();
        }
        ItemEvent?.Invoke(item.itemData, inventory[item.itemData]);
    }
}
