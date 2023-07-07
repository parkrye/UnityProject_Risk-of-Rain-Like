using System.Collections;
using UnityEngine;

/// <summary>
/// 패턴 1-1: 근접 공격
/// 패턴 1-2: 회복
/// 패턴 2-1: 점프 공격
/// 패턴 2-2: 돌진
/// </summary>
public class Golem : Boss
{
    protected override void Awake()
    {
        enemyData = GameManager.Resource.Load<EnemyData>("Boss/Golem");
        base.Awake();
    }

    protected override IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (attack)
            {
                switch (lifeMode)
                {
                    case LifeMode.Full:
                        switch (rangeMode)
                        {
                            case RangeMode.NearRange:
                                if (!patternCoroutineIsRunning)
                                    StartCoroutine(CloseAttackRoutine());
                                break;
                            case RangeMode.FarRange:
                                if (!patternCoroutineIsRunning)
                                    StartCoroutine(HealRoutine());
                                break;
                        }
                        break;
                    case LifeMode.Half:
                        switch (rangeMode)
                        {
                            case RangeMode.NearRange:
                                if (!patternCoroutineIsRunning)
                                    StartCoroutine(JumpAttackRoutine());
                                break;
                            case RangeMode.FarRange:
                                transform.LookAt(playerTransform);
                                if (!patternCoroutineIsRunning)
                                    StartCoroutine(DashAttackRoutine());
                                break;
                        }
                        break;
                }
                yield return new WaitForSeconds(enemyData.AttackSpeed);
            }
            yield return null;
        }
    }

    IEnumerator CloseAttackRoutine()
    {
        yield return null;
        patternCoroutineIsRunning = true;
        transform.LookAt(playerTransform);
        animator.SetTrigger("Attack");
        GameManager.Resource.Instantiate<ParticleSystem>("Particle/Explosion", attackTransform.position, Quaternion.identity, true);
        Collider[] colliders = Physics.OverlapSphere(attackTransform.position, enemyData.floatdatas[1]);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                IHitable hittable = colliders[i].GetComponent<IHitable>();
                hittable?.Hit(damage, 0f);
            }
        }
        animator.SetBool("SerialAttack", !animator.GetBool("SerialAttack"));
        patternCoroutineIsRunning = false;
    }

    IEnumerator HealRoutine()
    {
        patternCoroutineIsRunning = true;
        animator.SetBool("Heal", true);
        while (lifeMode == LifeMode.Full && rangeMode == RangeMode.FarRange)
        {
            HP += enemyData.floatdatas[0] * Time.deltaTime;
            yield return null;
        }
        animator.SetBool("Heal", false);
        patternCoroutineIsRunning = false;
    }

    IEnumerator JumpAttackRoutine()
    {
        patternCoroutineIsRunning = true;
        transform.LookAt(playerTransform);
        animator.SetTrigger("Jump");
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 100; i++)
        {
            transform.Translate(Vector3.up, Space.World);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(2f);
        transform.LookAt(playerTransform);
        Vector3 dir = playerTransform.position - transform.position;
        for (int i = 0; i < 50; i++)
        {
            transform.Translate(dir * 0.02f, Space.World);
            yield return new WaitForFixedUpdate();
        }
        GameManager.Resource.Instantiate<ParticleSystem>("Particle/Explosion", attackTransform.position, Quaternion.identity, true);
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyData.floatdatas[1]);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                IHitable hittable = colliders[i].GetComponent<IHitable>();
                hittable?.Hit(damage, 0f);
            }
        }
        transform.LookAt(playerTransform);
        patternCoroutineIsRunning = false;
    }

    IEnumerator DashAttackRoutine()
    {
        patternCoroutineIsRunning = true;
        animator.SetBool("Dash", true);
        yield return new WaitForSeconds(0.1f);
        Vector3 dir = playerTransform.position - transform.position;
        for (int i = 0; i < 50; i++)
        {
            transform.Translate(dir * 0.02f, Space.World);
            yield return new WaitForFixedUpdate();
        }
        GameManager.Resource.Instantiate<ParticleSystem>("Particle/Explosion", attackTransform.position, Quaternion.identity, true);
        Collider[] colliders = Physics.OverlapSphere(attackTransform.position, enemyData.floatdatas[1]);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                IHitable hittable = colliders[i].GetComponent<IHitable>();
                hittable?.Hit(damage, 0f);
            }
        }
        animator.SetBool("Dash", false);
        patternCoroutineIsRunning = false;
    }

    void OnDrawGizmos()
    {
        if (onGizmo)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(attackTransform.position, enemyData.floatdatas[1]);
        }
    }
}
