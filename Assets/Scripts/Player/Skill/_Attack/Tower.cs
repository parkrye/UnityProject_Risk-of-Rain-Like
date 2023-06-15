using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform shotTransform;
    public float damage, term, range;

    public void SetTower(float _damage, float _term, float _range)
    {
        damage = _damage;
        term = _term;
        range = _range;

        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject followBolt = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/FollowEnergyBolt"), shotTransform.position, Quaternion.identity, true);
            followBolt.GetComponent<FollowBolt>().Shot(1, damage, 0f, range);
            yield return new WaitForSeconds(term);
        }
        GameManager.Resource.Destroy(gameObject);
    }
}
