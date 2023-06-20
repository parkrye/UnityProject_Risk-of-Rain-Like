using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltType : MonoBehaviour
{
    protected TrailRenderer trail;
    protected float damage;
    protected Collider coll;
    [SerializeField] protected float speed;

    protected virtual void Awake()
    {
        trail = GetComponent<TrailRenderer>();
        coll = GetComponent<Collider>();
    }

    void OnEnable()
    {
        trail.enabled = false;
        coll.enabled = false;
    }

    public void Shot(Vector3 target, float _damage, float delay)
    {
        transform.LookAt(target);
        damage = _damage;
        StartCoroutine(ReadyToShot(delay));
    }

    public void Shot(Vector3 target, float _damage = 1f)
    {
        Shot(target, _damage, 0f);
    }

    protected virtual IEnumerator ReadyToShot(float delay)
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

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<IHitable>()?.Hit(damage);
            GameManager.Pool.Release(gameObject);
        }
    }
}
