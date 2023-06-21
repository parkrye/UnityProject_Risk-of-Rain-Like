using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    ItemData item;

    void Awake()
    {
        ItemData[] items = GameManager.Resource.LoadAll<ItemData>("Item");
        item = items[Random.Range(0, items.Length)];
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Data.Player.inventory.AddItem(item);
            GameManager.Resource.Destroy(gameObject);
        }
    }
}
