using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody rb;
    TrailRenderer trail;
    float damage;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
    }

    void OnEnable()
    {
        trail.enabled = false;
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
        trail.Clear();
        trail.enabled = true;
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        GameManager.Resource.Destroy(gameObject, 5f);
    }

    void OnDisable()
    {
        trail.Clear();
        rb.velocity = Vector3.zero;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<IHitable>()?.Hit(damage);
            GameManager.Pool.Release(gameObject);
        }
        else if(1 << other.gameObject.layer == LayerMask.GetMask("Ground"))
        {
            GameManager.Pool.Release(gameObject);
        }
    }
}
