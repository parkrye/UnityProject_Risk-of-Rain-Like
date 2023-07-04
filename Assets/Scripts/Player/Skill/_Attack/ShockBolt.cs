using UnityEngine;

public class ShockBolt : BoltType
{
    ParticleSystem fireworkParticle;
    public float range, stunTime;
    protected override void Awake()
    {
        base.Awake();
        fireworkParticle = GameManager.Resource.Load<ParticleSystem>("Particle/FireworkYellowSmall");
    }

    public void Shot(Vector3 target, float _damage, float delay, float _range, float _stunTime)
    {
        range = _range;
        stunTime = _stunTime;
        Shot(target, _damage, delay);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IHitable>()?.Hit(damage, 0f);
            Shock();
        }
        else if ((1 << other.gameObject.layer) == LayerMask.GetMask("Ground"))
        {
            Shock();
        }
    }

    void Shock()
    {
        GameManager.Resource.Instantiate<GameObject>("Audio/SFX/Shock");
        ParticleSystem effect = GameManager.Resource.Instantiate(fireworkParticle, transform.position, Quaternion.identity, true);
        GameManager.Resource.Destroy(effect.gameObject, 2f);
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        for(int i = 0; i < colliders.Length; i++)
        {
            IMezable mazable = colliders[i].GetComponent<IMezable>();
            mazable?.Stuned(stunTime);
        }
        GameManager.Resource.Destroy(gameObject);
    }
}
