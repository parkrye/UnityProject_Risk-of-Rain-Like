using UnityEngine;

[CreateAssetMenu(fileName = "GoldCoin", menuName = "Data/Item/GoldCoin")]
public class GoldCoin : ItemData
{
    public override void GetFirstEffect()
    {
        GameManager.Data.Player.coolTime *= 0.9f;
    }

    public override void GetNextEffect()
    {
        GameManager.Data.Player.coolTime *= 0.95f;
    }
}
