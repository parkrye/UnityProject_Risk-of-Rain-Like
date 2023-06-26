using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantBox : MonoBehaviour
{
    ItemData item;
    int cost;
    bool fall;

    void Awake()
    {
        ItemData[] items = GameManager.Resource.LoadAll<ItemData>("Item");
        cost = (int)((Random.Range(1, 10) + GameManager.Data.Time * 0.016f) * GameManager.Data.Difficulty);
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
            transform.Translate(Vector3.down * Time.deltaTime);
        transform.Rotate(Vector3.up * Time.deltaTime);
    }

    public void Interact()
    {
        if (GameManager.Data.Player.Coin >= cost)
        {
            GameManager.Data.Player.Coin -= cost;
            GameManager.Data.Player.inventory.AddItem(item);
            GameManager.Resource.Destroy(gameObject);
        }
    }
}
