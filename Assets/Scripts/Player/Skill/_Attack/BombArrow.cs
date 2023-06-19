using System.Collections;
using UnityEngine;

public class BombArrow : MonoBehaviour
{
    TrailRenderer trail;
    ParticleSystem bombParticle;

    float damage;
    [SerializeField] float speed, range;

    void Awake()
    {
        trail = GetComponent<TrailRenderer>();
        bombParticle = GameManager.Resource.Load<ParticleSystem>("Particle/Explosion");
    }

    void OnEnable()
    {
        trail.enabled = false;
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
        trail.Clear();
        trail.enabled = true;
        GameManager.Resource.Destroy(gameObject, 10f);
        while (true)
        {
            transform.Translate((transform.forward * speed + Vector3.up * Physics.gravity.y) * Time.deltaTime, Space.World);
            yield return new WaitForFixedUpdate();
        }
    }

    void OnDisable()
    {
        trail.Clear();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || (1 << other.gameObject.layer == LayerMask.GetMask("Ground")))
        {
            ParticleSystem effect = GameManager.Resource.Instantiate(bombParticle, transform.position, Quaternion.identity, true);
            GameManager.Resource.Destroy(effect.gameObject, 2f);

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
