using System.Collections;
using UnityEngine;

public class BombArrow : MonoBehaviour
{
    Rigidbody rb;
    TrailRenderer trail;
    ParticleSystem bombParticle;

    float damage, range;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
        bombParticle = GameManager.Resource.Load<ParticleSystem>("Particle/Explosion");
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
        if (other.tag == "Enemy" || (1 << other.gameObject.layer == LayerMask.GetMask("Ground")))
        {
            ParticleSystem effect = GameManager.Resource.Instantiate(bombParticle, transform.position, Quaternion.identity, true);
            GameManager.Resource.Destroy(effect.gameObject, 3f);

            Collider[] colliders = Physics.OverlapSphere(transform.position, range);
            foreach (Collider collider in colliders)
            {
                IHitable hitable = collider.GetComponent<IHitable>();
                hitable?.Hit(damage);
            }
            GameManager.Pool.Release(gameObject);
        }
    }
}
