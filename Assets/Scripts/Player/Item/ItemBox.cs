using System.Collections;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    protected ItemData[] items;
    protected ItemData item;
    protected bool fall, ground;

    protected virtual void OnEnable()
    {
        items = GameManager.Resource.LoadAll<ItemData>("Item");
        item = items[Random.Range(0, items.Length)];
        fall = true;
        ground = false;

        StartCoroutine(FallRoutine());
        StartCoroutine(TurnRoutine());
    }

    protected virtual IEnumerator FallRoutine()
    {
        while (fall)
        {
            if (ground)
                fall = false;
            yield return null;
        }
    }

    protected virtual IEnumerator TurnRoutine()
    {
        while (fall)
        {
            transform.Translate(10f * Time.deltaTime * Vector3.down);
            transform.Rotate(Vector3.up);
            yield return new WaitForFixedUpdate();
        }
    }

    public virtual void Interact()
    {
        GameManager.Data.Player.Inventory.AddItem(item);
        GameManager.Resource.Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer) == LayerMask.GetMask("Ground"))
        {
            ground = true;
        }
    }
}
