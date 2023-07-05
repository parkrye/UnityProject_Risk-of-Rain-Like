using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform shotTransform;
    float damage;
    [SerializeField] float term, range;

    public void SetTower(float _damage)
    {
        damage = _damage;

        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        for (int i = 0; i < 50; i++)
        {
            FollowBolt followBolt = GameManager.Resource.Instantiate<FollowBolt>("Attack/FollowEnergyBolt", shotTransform.position, Quaternion.identity, true);
            followBolt.Shot(transform.forward, damage, 0f);
            yield return new WaitForSeconds(term);
        }
        GameManager.Resource.Destroy(gameObject);
    }
}
