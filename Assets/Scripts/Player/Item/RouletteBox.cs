using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RouletteBox : MerchantBox
{
    [SerializeField] Image itemIamge;

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(ItemChangeRoutine());
    }

    IEnumerator ItemChangeRoutine()
    {
        while (this)
        {
            item = items[Random.Range(0, items.Length)];
            itemIamge.sprite = item.ItemIcon;
            yield return new WaitForSeconds(1f);
        }
    }
}
