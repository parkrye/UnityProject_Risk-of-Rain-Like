using System.Collections;
using UnityEngine;

public class TurtleShell : Enemy, IMezable
{
    EnemyBolt enemyNiddle;

    protected override void Awake()
    {
        enemyData = GameManager.Resource.Load<EnemyData>("Enemy/TurtleShell");
        enemyNiddle = GameManager.Resource.Load<EnemyBolt>("EnemyAttack/EnemyNiddle");
        base.Awake();
    }

    protected override IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (attack && !isStunned)
            {
                animator.SetTrigger("Attack");
                EnemyBolt niddle = GameManager.Resource.Instantiate(enemyNiddle, attackTransform.position, Quaternion.identity, true);
                niddle.Shot(GameManager.Data.Player.transform.position, damage);
                yield return new WaitForSeconds(enemyData.AttackSpeed);
            }
            else
            {
                yield return null;
            }
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
        float prevMoveSpeed = moveSpeed;
        float prevAttackSpeed = attackSpeed;
        moveSpeed *= modifier;
        attackSpeed *= modifier;
        yield return new WaitForSeconds(time);
        moveSpeed = prevMoveSpeed;
        attackSpeed = prevAttackSpeed;
        isSlowed = false;
    }

    public IEnumerator KnockBackRoutine(float distance, Transform backFrom)
    {
        float now = 0f;
        while (now < distance)
        {
            TranslateGradually(backFrom.forward, distance * Time.deltaTime);
            now += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}
