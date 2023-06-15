using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IHitable
{
    public EnemyData enemyData;
    protected Enemy_AI enemy_AI;
    [SerializeField] ParticleSystem bleedParticle;

    public float hp;
    public bool bleed, attack;

    protected virtual void Awake()
    {
        enemy_AI = GetComponent<Enemy_AI>();
        bleedParticle = GameManager.Resource.Load<ParticleSystem>("Particle/Bleed");
    }

    void OnEnable()
    {
        enemy_AI.CreateBehaviorTreeAIState();
        hp = enemyData.MaxHP;
        bleed = false;
        attack = false;
        StartCoroutine(BleedingRoutine());
        StartCoroutine(AttackRoutine());
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
    }

    public void Die()
    {
        GameManager.Resource.Destroy(gameObject);
    }

    IEnumerator BleedingRoutine()
    {
        while (true)
        {
            bleed = true;
            yield return new WaitForSeconds(1f);
        }
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
