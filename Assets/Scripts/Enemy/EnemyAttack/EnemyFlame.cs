using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlame : MonoBehaviour
{
    public float damage;

    public void Shot(float _damage, Transform shotPoiont)
    {
        damage = _damage;
        transform.position = shotPoiont.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<IHitable>()?.Hit(damage);
            GameManager.Pool.Release(gameObject);
        }
    }
}
