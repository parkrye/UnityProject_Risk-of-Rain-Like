using UnityEngine;

public class SlowEnergyBolt : BoltType
{
    float slowTime, slowModifier;

    public void Shot(Vector3 target, float damage, float delay, float _slowTime, float _slowModifier)
    {
        slowTime = _slowTime;
        slowModifier = _slowModifier;
        Shot(target, damage, delay);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IHitable>()?.Hit(damage, 0f);
            other.GetComponent<IMezable>()?.Slowed(slowTime, slowModifier);
            GameManager.Resource.Destroy(gameObject);
        }
        else if ((1 << other.gameObject.layer) == LayerMask.GetMask("Ground"))
        {
            GameManager.Resource.Destroy(gameObject);
        }
    }
}
