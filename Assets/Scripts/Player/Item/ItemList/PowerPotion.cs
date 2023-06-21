using UnityEngine;

[CreateAssetMenu(fileName = "PowerPotion", menuName = "Data/Item/PowerPotion")]
public class PowerPotion : ItemData
{
    public override void GetFirstEffect()
    {
        GameManager.Data.Player.attackDamage *= 1.1f;
    }

    public override void GetNextEffect()
    {
        GameManager.Data.Player.attackDamage *= 1.05f;
    }
}