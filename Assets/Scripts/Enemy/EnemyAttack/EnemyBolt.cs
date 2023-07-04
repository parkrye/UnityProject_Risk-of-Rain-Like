using UnityEngine;

public class EnemyBolt : BoltType
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IHitable>()?.Hit(damage, 0f);
            GameManager.Resource.Destroy(gameObject);
        }
        else if((1 << other.gameObject.layer) == LayerMask.GetMask("Ground"))
        {
            GameManager.Resource.Destroy(gameObject);
        }
    }
}
