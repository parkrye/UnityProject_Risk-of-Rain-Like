using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerForTest : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(1f);
            ItemData itemData = GameManager.Resource.Load<ItemData>("Item/HPCoffee");
            GameManager.Resource.Instantiate(itemData.ItemObject, transform.position, transform.rotation, true);
        }
    }
}
