using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    public ItemData itemData;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.Data.Player.inventory.AddItem(this);
            GameManager.Resource.Destroy(gameObject);
        }
    }

    public abstract void GetFirstEffect();

    public abstract void GetNextEffect();
}
