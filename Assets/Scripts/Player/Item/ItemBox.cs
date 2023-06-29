using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    ItemData item;
    bool fall;

    void Awake()
    {
        ItemData[] items = GameManager.Resource.LoadAll<ItemData>("Item");
        item = items[Random.Range(0, items.Length)];
        fall = true;
    }

    void Update()
    {
        if (fall)
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(ray, 2f, LayerMask.GetMask("Ground")))
                fall = false;
        }
    }

    void FixedUpdate()
    {
        if (fall)
            transform.Translate(Vector3.down * Time.deltaTime * 10f);
        transform.Rotate(Vector3.up);
    }

    public void Interact()
    {
        GameManager.Data.Player.Inventory.AddItem(item);
        GameManager.Resource.Destroy(gameObject);
    }
}
