using System.Collections;
using UnityEngine;

public class FollowBolt : MonoBehaviour
{
    TrailRenderer trail;
    float damage, range;
    [SerializeField] GameObject target;
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

    public void Shot(float speed, float _damage, float delay, float _range)
    {
        damage = _damage;
        range = _range;
        StartCoroutine(ReadyToShot(speed, delay));
    }

    IEnumerator ReadyToShot(float speed, float delay)
    {
        yield return new WaitForSeconds(delay);
        coll.enabled = true;
        trail.Clear();
        trail.enabled = true;
        GameManager.Resource.Destroy(gameObject, 5f);
        while (true)
        {
            if (target == null || target.activeInHierarchy || target.activeSelf)
            {
                if (SetTarget())
                {
                    transform.Translate((target.transform.position - transform.position) * speed * Time.deltaTime, Space.World);
                }
                else
                {
                    transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
                }
            }
            else
            {
                transform.Translate((target.transform.position - transform.position) * speed * Time.deltaTime, Space.World);
            }
                
            yield return new WaitForFixedUpdate();
        }
    }

    bool SetTarget()
    {
        PriorityQueue<GameObject, float> pq = new PriorityQueue<GameObject, float>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                pq.Enqueue(collider.gameObject, Vector3.Distance(transform.position, collider.transform.position));
            }
        }
        if(pq.Count > 0)
        {
            target = pq.Dequeue();
            return true;
        }
        return false;
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
