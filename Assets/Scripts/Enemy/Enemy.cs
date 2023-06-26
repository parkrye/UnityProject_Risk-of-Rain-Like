using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour, IHitable, IMazable
{
    public EnemyData enemyData;
    [SerializeField] ParticleSystem bleedParticle;
    protected Animator animator;

    public float hp, damage;
    public bool bleed, attack;
    [SerializeField] bool onGizmo;
    public bool isStunned, isSlowed;

    public UnityEvent OnEnemyDieEvent;
    public UnityEvent<float> OnHPEvent;

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
        StopCoroutine(AttackRoutine());
        StartCoroutine(BleedingRoutine());
    }

    void OnDisable()
    {
        OnEnemyDieEvent?.Invoke();
    }

    public void Hit(float damage)
    {
        hp -= damage;
        OnHPEvent?.Invoke(hp);
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
        StopAttack();
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
        GameManager.Data.Player.Coin += (int)((enemyData.Coin + GameManager.Data.Time * 0.0016f) / (GameManager.Data.Difficulty));
        GameManager.Data.Player.EXP += (int)((enemyData.Exp + GameManager.Data.Time * 0.0016f) / (GameManager.Data.Difficulty));
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

    public virtual void StopAttack()
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

    public void Stuned(float time)
    {
        if(!isStunned)
        {
            StartCoroutine(StunRoutine(time));
        }
    }

    public void Slowed(float time, float modifier)
    {
        if (!isSlowed)
        {
            StartCoroutine(SlowRoutine(time, modifier));
        }
    }

    public void KnockBack(float distance, Transform backFrom)
    {
        StartCoroutine(KnockBackRoutine(distance, backFrom));
    }

    public IEnumerator StunRoutine(float time)
    {
        isStunned = true;
        animator.SetBool("Stun", isStunned);
        StopAttack();
        StopCoroutine(AttackRoutine());
        yield return new WaitForSeconds(time);
        isStunned = false;
        animator.SetBool("Stun", isStunned);
        StartAttack();
        StartCoroutine(AttackRoutine());
    }

    public IEnumerator SlowRoutine(float time, float modifier)
    {
        isSlowed = true;
        float prevMoveSpeed = enemyData.MoveSpeed;
        float prevAttackSpeed = enemyData.AttackSpeed;
        enemyData.MoveSpeed *= modifier;
        enemyData.AttackSpeed *= modifier;
        yield return new WaitForSeconds(time);
        enemyData.MoveSpeed = prevMoveSpeed;
        enemyData.AttackSpeed = prevAttackSpeed;
        isSlowed = false;
    }

    public IEnumerator KnockBackRoutine(float distance, Transform backFrom)
    {
        float now = 0f;
        while(now < distance)
        {
            transform.Translate(backFrom.forward * Time.deltaTime);
            now += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}
