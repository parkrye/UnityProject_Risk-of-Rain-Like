using UnityEngine;

[CreateAssetMenu(fileName = "SharpDagger", menuName = "Data/Item/SharpDagger")]
public class SharpDagger : ItemData
{
    public override void GetFirstEffect()
    {
        GameManager.Data.Player.CriticalRatio += 1f;
    }

    public override void GetNextEffect()
    {
        GameManager.Data.Player.CriticalRatio *= 1.1f;
    }
}
