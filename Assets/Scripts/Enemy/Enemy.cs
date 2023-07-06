using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour, IHitable, ITranslatable
{
    public EnemyData enemyData;
    protected Animator animator;
    public Transform attackTransform;
    [SerializeField] ParticleSystem bleedParticle;
    [SerializeField] AudioSource hitAudio;
    SphereCollider sphere;
    protected IEnumerator Attack;

    public float hp, damage, moveSpeed, attackSpeed;
    public float HP 
    { 
        get { return hp; } 
        set 
        { 
            if (value > enemyData.MaxHP) 
                hp = enemyData.MaxHP;  
            else
                hp = value;
        } 
    }
    public bool bleed, attack;
    [SerializeField] public bool onGizmo;
    public bool isStunned, isSlowed, alive;
    int groundCount;

    public Vector3 EnemyPos
    {
        get { return transform.position + Vector3.up * enemyData.yModifier; }
    }

    public UnityEvent OnEnemyDieEvent;
    public UnityEvent<float> OnHPEvent;

    protected virtual void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        sphere = GetComponent<SphereCollider>();
        Attack = AttackRoutine();
    }

    protected virtual void OnEnable()
    {
        HP = enemyData.MaxHP * (1 + (GameManager.Data.NowRecords["Difficulty"] - 1) * 0.5f + GameManager.Data.NowRecords["Time"] * 0.0016f);
        damage = enemyData.Damage * (1 + (GameManager.Data.NowRecords["Difficulty"] - 1) * 0.5f + GameManager.Data.NowRecords["Time"] * 0.0016f);
        moveSpeed = enemyData.MoveSpeed;
        attackSpeed = enemyData.AttackSpeed;
        bleed = false;
        attack = false;
        alive = true;
        groundCount = 0;
        sphere.radius = enemyData.Size;
        sphere.center = Vector3.up * enemyData.yModifier;
        bleedParticle.transform.position = Vector3.up * enemyData.yModifier;
        StopAllCoroutines();
        StartCoroutine(BleedingRoutine());
        StartCoroutine(Attack);
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
        while (nowTime <= time)
        {
            HP -= damage;
            GameManager.Data.NowRecords["Damage"] += damage;
            OnHPEvent?.Invoke(HP);
            if (bleed)
            {
                hitAudio.Play();
                bleedParticle.Play();
                bleed = false;
            }
            if (HP <= 0f)
            {
                if (alive)
                {
                    alive = false;
                    Die();
                }
            }
            else
            {
                animator.SetTrigger("Hit");
            }
            nowTime += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Die()
    {
        GameManager.Data.NowRecords["Kill"] += 1;
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
        GameManager.Data.Player.Coin += (int)((enemyData.Coin + GameManager.Data.NowRecords["Time"] * 0.0016f) * ( 4 - (GameManager.Data.NowRecords["Difficulty"])));
        GameManager.Data.Player.EXP += (int)((enemyData.Exp + GameManager.Data.NowRecords["Time"] * 0.0016f) * ( 4 - (GameManager.Data.NowRecords["Difficulty"])));
        if (Random.Range(0, 10) <= 3 - GameManager.Data.NowRecords["Difficulty"])
            GameManager.Resource.Instantiate<ItemBox>("Item/ItemBox", transform.position, Quaternion.identity);
        GameManager.Resource.Destroy(gameObject);
    }

    public virtual void StartAttack()
    {
        if(HP > 0)
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
        transform.position += dir * distance;
        if (groundCount > 0)
        {
            transform.position -= dir * distance;
            return false;
        }
        return true;
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
            Vector3 prevPos = transform.position;
            transform.position = pos;
            if (groundCount > 0)
            {
                transform.position = prevPos;
                return false;
            }
            return true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer) == LayerMask.GetMask("Ground"))
        {
            groundCount++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer) == LayerMask.GetMask("Ground"))
        {
            groundCount--;
        }
    }
}
