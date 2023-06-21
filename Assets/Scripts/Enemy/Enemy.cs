using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IHitable
{
    public EnemyData enemyData;
    protected Enemy_AI enemy_AI;
    [SerializeField] ParticleSystem bleedParticle;
    protected Animator animator;

    public float hp, damage;
    public bool bleed, attack;

    protected virtual void Awake()
    {
        enemy_AI = GetComponent<Enemy_AI>();
        bleedParticle = GameManager.Resource.Load<ParticleSystem>("Particle/Bleed");
        animator = gameObject.GetComponent<Animator>();
    }

    void OnEnable()
    {
        enemy_AI.CreateBehaviorTreeAIState();
        hp = enemyData.MaxHP * (1 + (GameManager.Data.difficulty - 1) * 0.5f + GameManager.Data.time * 0.0016f);
        damage = enemyData.Damage * (1 + (GameManager.Data.difficulty - 1) * 0.5f + GameManager.Data.time * 0.0016f);
        bleed = false;
        attack = false;
        GetComponent<SphereCollider>().radius = enemyData.size;
        StartCoroutine(BleedingRoutine());
        StartCoroutine(AttackRoutine());
    }

    public void Hit(float damage)
    {
        animator.SetTrigger("Hit");
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
    }

    public void Die()
    {
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
        GameManager.Data.Player.EXP += enemyData.exp / (GameManager.Data.difficulty);
        if (Random.Range(0, 10) == 0)
            GameManager.Resource.Instantiate<ItemBox>("Item/ItemBox", transform.position, Quaternion.identity);
        GameManager.Resource.Destroy(gameObject);
    }

    public void StartAttack()
    {
        attack = true;
    }

    public void StopAttack()
    {
        attack = false;
    }

    protected abstract IEnumerator AttackRoutine();
}
