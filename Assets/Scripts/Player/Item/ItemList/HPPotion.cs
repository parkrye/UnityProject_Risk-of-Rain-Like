using UnityEngine;

[CreateAssetMenu(fileName = "HPPotion", menuName = "Data/Item/HPPotion")]
public class HPPotion : ItemData
{
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
