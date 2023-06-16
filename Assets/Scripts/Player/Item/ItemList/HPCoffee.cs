using UnityEngine;

public class HPCoffee : ItemBase
{
    void Awake()
    {
        itemData = GameManager.Resource.Load<ItemData>("Item/HPCoffee");
    }

    public override void GetFirstEffect()
    {
        GameManager.Data.Player.MAXHP += 10f;
        GameManager.Data.Player.NOWHP += 10f;
    }

    public override void GetNextEffect()
    {
        GameManager.Data.Player.MAXHP += 10f;
        GameManager.Data.Player.NOWHP += 10f;
    }
}
