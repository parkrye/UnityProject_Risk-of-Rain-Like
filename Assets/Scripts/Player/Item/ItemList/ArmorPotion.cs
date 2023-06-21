using UnityEngine;

[CreateAssetMenu(fileName = "ArmorPotion", menuName = "Data/Item/ArmorPotion")]
public class ArmorPotion : ItemData
{
    public override void GetFirstEffect()
    {
        GameManager.Data.Player.armorPoint *= 0.9f;
    }

    public override void GetNextEffect()
    {
        GameManager.Data.Player.armorPoint *= 0.95f;
    }
}
