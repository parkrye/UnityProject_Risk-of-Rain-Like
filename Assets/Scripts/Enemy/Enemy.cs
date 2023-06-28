using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour, IHitable, ITranslatable
{
    public EnemyData enemyData;
    [SerializeField] ParticleSystem bleedParticle;
    protected Animator animator;

    public float hp, damage, moveSpeed, attackSpeed;
    public bool bleed, attack;
    [SerializeField] bool onGizmo;
    public bool isStunned, isSlowed;

    public Vector3 enemyPos
    {
        get { return transform.position + Vector3.up * enemyData.yModifier; }
    }

    public UnityEvent OnEnemyDieEvent;
    public UnityEvent<float> OnHPEvent;

    protected virtual void Awake()
    {
        bleedParticle = GameManager.Resource.Load<ParticleSystem>("Particle/Bleed");
        animator = gameObject.GetComponent<Animator>();
    }

    void OnEnable()
    {
        hp = enemyData.MaxHP * (1 + (GameManager.Data.Records["Difficulty"] - 1) * 0.5f + GameManager.Data.Records["Time"] * 0.0016f);
        damage = enemyData.Damage * (1 + (GameManager.Data.Records["Difficulty"] - 1) * 0.5f + GameManager.Data.Records["Time"] * 0.0016f);
        moveSpeed = enemyData.MoveSpeed;
        attackSpeed = enemyData.AttackSpeed;
        bleed = false;
        attack = false;
        GetComponent<SphereCollider>().radius = enemyData.Size;
        StopCoroutine(AttackRoutine());
        StartCoroutine(BleedingRoutine());
        StartCoroutine(AttackRoutine());
    }

    void OnDisable()
    {
        OnEnemyDieEvent?.Invoke();
    }

    public void Hit(float damage, float Time)
    {
        StartCoroutine(HitRoutine(damage, Time));
    }

    public IEnumerator HitRoutine(float damage, float time)
    {
        float nowTime = 0f;
        do
        {
            hp -= damage;
            GameManager.Data.Records["Damage"] += damage;
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
            nowTime += 0.1f;
            yield return new WaitForSeconds(0.1f);
        } while (nowTime < time);
    }

    public void Die()
    {
        GameManager.Data.Records["Kill"] += 1;
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
        GameManager.Data.Player.Coin += (int)((enemyData.Coin + GameManager.Data.Records["Time"] * 0.0016f) / (GameManager.Data.Records["Difficulty"]));
        GameManager.Data.Player.EXP += (int)((enemyData.Exp + GameManager.Data.Records["Time"] * 0.0016f) / (GameManager.Data.Records["Difficulty"]));
        if (Random.Range(0, 10) <= 3 - GameManager.Data.Records["Difficulty"])
            GameManager.Resource.Instantiate<ItemBox>("Item/ItemBox", transform.position, Quaternion.identity);
        GameManager.Resource.Destroy(gameObject);
    }

    public virtual void StartAttack()
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
            Gizmos.DrawWireSphere(transform.position + Vector3.up * enemyData.Size, enemyData.Range);
        }
    }

    protected abstract IEnumerator AttackRoutine();

    public bool TranslateGradually(Vector3 dir, float distance)
    {
        if (!Physics.SphereCast(enemyPos, enemyData.Size, dir, out _, distance))
        {
            transform.position += dir * distance;
            return true;
        }
        return false;
    }

    public bool TranslateSuddenly(Vector3 pos, bool ignoreGround = true)
    {
        if (ignoreGround)
        {
            transform.position = pos;
            return true;
        }
        else
        {
            if (!Physics.SphereCast(enemyPos, enemyData.Size, Vector3.up, out _, 0f))
            {
                transform.position = pos;
                return true;
            }
            return false;
        }
    }
}
