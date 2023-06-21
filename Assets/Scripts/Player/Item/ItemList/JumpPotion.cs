using UnityEngine;

[CreateAssetMenu (fileName ="JumpPotion", menuName ="Data/Item/JumpPotion")]
public class JumpPotion : ItemData
{
    public override void GetFirstEffect()
    {
        GameManager.Data.Player.jumpLimit++;
    }

    public override void GetNextEffect()
    {
        GameManager.Data.Player.jumpLimit++;
    }
}
