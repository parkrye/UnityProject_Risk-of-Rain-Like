using System.Collections;
using UnityEngine;

public class Bat : Enemy, IMezable
{
    EnemyBolt enemyBolt;

    protected override void Awake()
    {
        enemyData = GameManager.Resource.Load<EnemyData>("Enemy/Bat");
        enemyBolt = GameManager.Resource.Load<EnemyBolt>("EnemyAttack/EnemyBolt");
        base.Awake();
    }

    protected override IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (attack && !isStunned)
            {
                animator.SetTrigger("Attack");
                EnemyBolt boltAttack = GameManager.Resource.Instantiate(enemyBolt, attackTransform.position, Quaternion.identity, true);
                boltAttack.Shot(GameManager.Data.Player.transform.position, damage);
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
            //transform.Translate(distance * Time.deltaTime * backFrom.forward, Space.World);
            now += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}
