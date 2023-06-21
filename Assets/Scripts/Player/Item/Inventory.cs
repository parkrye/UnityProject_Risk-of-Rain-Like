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
        ItemEvent?.Invoke(item, inventory[item]);
    }
}
