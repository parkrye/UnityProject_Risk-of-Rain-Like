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

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * Time.deltaTime);
    }

    public void Interact()
    {
        GameManager.Data.Player.inventory.AddItem(item);
        GameManager.Resource.Destroy(gameObject);
    }
}
