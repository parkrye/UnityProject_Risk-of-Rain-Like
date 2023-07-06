using System.Collections;
using UnityEngine;

public class Dragon : Enemy, IMezable
{
    [SerializeField] Transform mouse;
    bool attacking;
    EnemyFlame enemyFlame, flameAttack;
    IEnumerator Breath;

    protected override void Awake()
    {
        attacking = false;
        enemyData = GameManager.Resource.Load<EnemyData>("Enemy/Dragon");
        enemyFlame = GameManager.Resource.Load<EnemyFlame>("EnemyAttack/EnemyFlame");
        Breath = BreathRoutine();
        base.Awake();
    }

    protected override IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (attack && !attacking && !isStunned)
            {
                animator.SetTrigger("Attack");
                StartCoroutine(Breath);
                attacking = true;
            }
            yield return null;
        }
    }

    IEnumerator BreathRoutine()
    {
        flameAttack = GameManager.Resource.Instantiate(enemyFlame, attackTransform.position, Quaternion.identity, transform);
        flameAttack.transform.LookAt(attackTransform.position + transform.forward);
        flameAttack.Shot(damage);
        yield return new WaitForSeconds(enemyData.floatdatas[0]);
        GameManager.Resource.Destroy(flameAttack);
        yield return new WaitForSeconds(enemyData.floatdatas[1]);
        attacking = false;
    }

    public override void StopAttack()
    {
        base.StopAttack();
        if (isStunned)
        {
            attacking = false;
            StopCoroutine(Breath);
            GameManager.Resource.Destroy(flameAttack);
        }
    }

    public void Stuned(float time)
    {
        if (!isStunned)
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
        StopCoroutine(Attack);
        yield return new WaitForSeconds(time);
        isStunned = false;
        animator.SetBool("Stun", isStunned);
        StartAttack();
        StartCoroutine(Attack);
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
        while (now < distance)
        {
            TranslateGradually(backFrom.forward, distance * Time.deltaTime);
            now += Time.deltaTime * distance;
            yield return new WaitForFixedUpdate();
        }
    }
}
