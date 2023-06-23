using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour, IHitable
{
    public EnemyData enemyData;
    [SerializeField] ParticleSystem bleedParticle;
    protected Animator animator;

    public float hp, damage;
    public bool bleed, attack;
    [SerializeField] bool onGizmo;

    public UnityEvent EnemyDie;

    protected virtual void Awake()
    {
        bleedParticle = GameManager.Resource.Load<ParticleSystem>("Particle/Bleed");
        animator = gameObject.GetComponent<Animator>();
    }

    void OnEnable()
    {
        hp = enemyData.MaxHP * (1 + (GameManager.Data.Difficulty - 1) * 0.5f + GameManager.Data.Time * 0.0016f);
        damage = enemyData.Damage * (1 + (GameManager.Data.Difficulty - 1) * 0.5f + GameManager.Data.Time * 0.0016f);
        bleed = false;
        attack = false;
        GetComponent<SphereCollider>().radius = enemyData.Size;
        StartCoroutine(BleedingRoutine());
        StartCoroutine(AttackRoutine());
    }

    void OnDisable()
    {
        EnemyDie?.Invoke();
    }

    public void Hit(float damage)
    {
        hp -= damage;
        if (bleed)
        {
            ParticleSystem effect = GameManager.Resource.Instantiate(bleedParticle, transform.position, Quaternion.identity, true);
            GameManager.Resource.Destroy(effect.gameObject, 2f);
            bleed = false;
        }
        if (hp <= 0f)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("Hit");
        }
    }

    public void Die()
    {
        StopAllCoroutines();
        StartCoroutine(DieRoutine());
    }

    IEnumerator BleedingRoutine()
    {
        while (true)
        {
            bleed = true;
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator DieRoutine()
    {
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(1f);
        GameManager.Data.Player.EXP += enemyData.Exp / (GameManager.Data.Difficulty);
        if (Random.Range(0, 10) <= 3 - GameManager.Data.Difficulty)
            GameManager.Resource.Instantiate<ItemBox>("Item/ItemBox", transform.position, Quaternion.identity);
        GameManager.Resource.Destroy(gameObject);
    }

    public void StartAttack()
    {
        if(hp > 0)
        {
            attack = true;
        }
    }

    public void StopAttack()
    {
        attack = false;
    }

    void OnDrawGizmos()
    {
        if (onGizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + Vector3.up, enemyData.Range);
        }
    }

    protected abstract IEnumerator AttackRoutine();
}
