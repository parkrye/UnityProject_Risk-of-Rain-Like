using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlame : MonoBehaviour
{
    public float damage;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<IHitable>()?.Hit(damage);
            GameManager.Pool.Release(gameObject);
        }
    }
}
