using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    float damage;

    public void StartFlame(float _damage)
    {
        damage = _damage;
    }

    public void StopFlame()
    {
        GameManager.Resource.Destroy(gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<IHitable>()?.Hit(damage);
        }
    }
}
