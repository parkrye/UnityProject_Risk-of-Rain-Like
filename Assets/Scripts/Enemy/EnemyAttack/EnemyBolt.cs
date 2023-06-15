using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBolt : MonoBehaviour
{
    TrailRenderer trail;
    float damage;
    Collider coll;

    void Awake()
    {
        trail = GetComponent<TrailRenderer>();
        coll = GetComponent<Collider>();
    }

    void OnEnable()
    {
        trail.enabled = false;
        coll.enabled = false;
    }

    public void Shot(float speed, float _damage, float delay)
    {
        damage = _damage;
        StartCoroutine(ReadyToShot(speed, delay));
    }

    public void Shot(float speed, float _damage = 1f)
    {
        Shot(speed, _damage, 0f);
    }

    IEnumerator ReadyToShot(float speed, float delay)
    {
        yield return new WaitForSeconds(delay);
        coll.enabled = true;
        trail.Clear();
        trail.enabled = true;
        GameManager.Resource.Destroy(gameObject, 10f);
        while (true)
        {
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
            yield return new WaitForFixedUpdate();
        }
    }

    void OnDisable()
    {
        trail.Clear();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<IHitable>()?.Hit(damage);
            GameManager.Pool.Release(gameObject);
        }
        else if((1 << other.gameObject.layer) == LayerMask.GetMask("Ground"))
        {
            GameManager.Pool.Release(gameObject);
        }
    }
}
