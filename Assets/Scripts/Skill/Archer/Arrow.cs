using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody rb;
    TrailRenderer trail;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
    }

    void OnEnable()
    {
        trail.Clear();
    }

    public void Shot(float speed, float damage, float delay)
    {
        StartCoroutine(ReadyToShot(speed, damage, delay));
    }

    public void Shot(float speed, float damage = 1f)
    {
        Shot(speed, damage, 0f);
    }

    IEnumerator ReadyToShot(float speed, float damage, float delay)
    {
        yield return new WaitForSeconds(delay);
        trail.enabled = true;
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        GameManager.Resource.Destroy(gameObject, 5f);
    }

    void OnDisable()
    {
        trail.enabled = false;
        rb.velocity = Vector3.zero;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            GameManager.Pool.Release(gameObject);
        }
    }
}
