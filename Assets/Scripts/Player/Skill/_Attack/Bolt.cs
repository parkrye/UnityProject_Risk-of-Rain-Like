using System.Collections;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    TrailRenderer trail;
    float damage;
    Collider coll;
    [SerializeField] float speed;

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

    public void Shot(float _damage, float delay)
    {
        damage = _damage;
        StartCoroutine(ReadyToShot(delay));
    }

    public void Shot(float _damage = 1f)
    {
        Shot(_damage, 0f);
    }

    IEnumerator ReadyToShot(float delay)
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
        if (other.tag == "Enemy")
        {
            other.GetComponent<IHitable>()?.Hit(damage);
            GameManager.Pool.Release(gameObject);
        }
    }
}
