using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlame : MonoBehaviour
{
    public float damage;

    public void Shot(float _damage)
    {
        damage = _damage;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<IHitable>()?.Hit(damage);
        }
    }
}
