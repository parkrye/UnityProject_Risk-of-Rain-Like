using UnityEngine;

[CreateAssetMenu(fileName = "HyperLens", menuName = "Data/Item/HyperLens")]
public class HyperLens : ItemData
{
    public override void GetFirstEffect()
    {
        GameManager.Data.Player.CriticalProbability += 1f;
    }

    public override void GetNextEffect()
    {
        GameManager.Data.Player.CriticalProbability += 1f;
    }
}

