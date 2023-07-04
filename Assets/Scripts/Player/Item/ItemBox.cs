using System.Collections;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    protected ItemData item;
    protected bool fall;

    protected virtual void OnEnable()
    {
        ItemData[] items = GameManager.Resource.LoadAll<ItemData>("Item");
        item = items[Random.Range(0, items.Length)];
        fall = true;

        StartCoroutine(FallRoutine());
        StartCoroutine(TurnRoutine());
    }

    protected virtual IEnumerator FallRoutine()
    {
        while (fall)
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(ray, 2f, LayerMask.GetMask("Ground")))
                fall = false;
            yield return null;
        }
    }

    protected virtual IEnumerator TurnRoutine()
    {
        while (fall)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 10f);
            transform.Rotate(Vector3.up);
            yield return new WaitForFixedUpdate();
        }
    }

    public virtual void Interact()
    {
        GameManager.Data.Player.Inventory.AddItem(item);
        GameManager.Resource.Destroy(gameObject);
    }
}
