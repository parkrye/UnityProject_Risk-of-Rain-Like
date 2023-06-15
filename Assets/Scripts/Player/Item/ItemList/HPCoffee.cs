using UnityEngine;

[CreateAssetMenu(fileName = "HPCoffee", menuName = "Data/Item/HPCoffee")]
public class HPCoffee : ItemData
{
    public override void GetFirstEffect()
    {
        GameManager.Data.Player.maxHP += 10f;
        GameManager.Data.Player.nowHP += 10f;
    }

    public override void GetNextEffect()
    {
        GameManager.Data.Player.maxHP += 10f;
        GameManager.Data.Player.nowHP += 10f;
    }
}
