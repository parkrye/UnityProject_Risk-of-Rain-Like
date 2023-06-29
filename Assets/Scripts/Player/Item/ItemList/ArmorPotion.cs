using UnityEngine;

[CreateAssetMenu(fileName = "ArmorPotion", menuName = "Data/Item/ArmorPotion")]
public class ArmorPotion : ItemData, IDamageSubscriber
{
    [SerializeField] float armorRatio;
    public override void GetFirstEffect()
    {
        GameManager.Data.Player.playerSystem.AddDamageSubscriber(this);
        armorRatio = 0.9f;
    }

    public override void GetNextEffect()
    {
        armorRatio *= 0.9f;
    }

    public float ModifiyDamage(float _damage)
    {
        return armorRatio * _damage;
    }
}
