using TMPro;
using UnityEngine;
using System.Collections;

public class MerchantBox : ItemBox
{
    protected int cost;
    [SerializeField] protected GameObject costObject;

    protected override void OnEnable()
    {
        base.OnEnable();
        cost = (int)((Random.Range(1, 10) + GameManager.Data.NowRecords["Time"] * 0.016f) * GameManager.Data.NowRecords["Difficulty"]);
        fall = true;
        costObject.GetComponentInChildren<TextMeshProUGUI>().text = cost.ToString();

        StartCoroutine(LookRoutine());
    }

    protected virtual IEnumerator LookRoutine()
    {
        while (this)
        {
            costObject.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
            yield return new WaitForEndOfFrame();
        }
    }

    public override void Interact()
    {
        if (GameManager.Data.Player.Coin >= cost)
        {
            GameManager.Data.Player.Coin -= cost;
            GameManager.Data.Player.Inventory.AddItem(item);
            GameManager.Resource.Destroy(gameObject);
        }
    }
}
