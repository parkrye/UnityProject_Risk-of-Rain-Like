using System.Collections;
using UnityEngine;

public class Dragon : Enemy, IMazable
{
    [SerializeField] Transform mouse;
    bool attacking;
    GameObject enemyFlame;
    protected override void Awake()
    {
        attacking = false;
        enemyData = GameManager.Resource.Load<EnemyData>("Enemy/Dragon");
        base.Awake();
    }

    protected override IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (attack && !attacking && !isStunned)
            {
                animator.SetTrigger("Attack");
                StartCoroutine(Breath());
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator Breath()
    {
        attacking = true;
        enemyFlame = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("EnemyAttack/EnemyFlame"), mouse.position, Quaternion.identity, transform);
        enemyFlame.transform.LookAt(mouse.position + transform.forward);
        enemyFlame.GetComponent<EnemyFlame>().Shot(damage);
        yield return new WaitForSeconds(enemyData.floatdatas[0]);
        GameManager.Resource.Destroy(enemyFlame);
        yield return new WaitForSeconds(enemyData.floatdatas[1]);
        attacking = false;
    }

    public override void StopAttack()
    {
        base.StopAttack();
        if (isStunned)
        {
            attacking = false;
            StopCoroutine(Breath());
            GameManager.Resource.Destroy(enemyFlame);
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
        Vector3 knockBackVector = backFrom.forward;
        while (now < distance)
        {
            transform.Translate(distance * Time.deltaTime * knockBackVector, Space.World);
            now += Time.deltaTime * distance;
            yield return new WaitForFixedUpdate();
        }
    }
}
