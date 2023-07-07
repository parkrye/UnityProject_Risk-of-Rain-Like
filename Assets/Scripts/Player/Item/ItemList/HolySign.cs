using UnityEngine;

[CreateAssetMenu(fileName = "HolySign", menuName = "Data/Item/HolySign")]
public class HolySign : ItemData, IDamageSubscriber
{
    [SerializeField] float blockProbability;
    public override void GetFirstEffect()
    {
        GameManager.Data.Player.playerSystem.AddDamageSubscriber(this);
        blockProbability = 1f;
    }

    public override void GetNextEffect()
    {
        blockProbability *= 1.1f;
    }

    public float ModifiyDamage(float _damage)
    {
        if(Random.Range(0f, 10f) < blockProbability)
            return 0f;
        return _damage;
    }
}
