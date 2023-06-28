using UnityEngine;

public class PoisonBolt : BoltType
{
    float time;

    public void Shot(Vector3 target, float damage, float delay, float _time)
    {
        time = _time;
        Shot(target, damage, delay);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IHitable>()?.Hit(damage, time);
            GameManager.Resource.Destroy(gameObject);
        }
        else if ((1 << other.gameObject.layer) == LayerMask.GetMask("Ground"))
        {
            GameManager.Resource.Destroy(gameObject);
        }
    }
}
