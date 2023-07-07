using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 지상 몬스터 AI
/// 접근 => 공격
/// </summary>
public class EnemyAI_TypeA2 : EnemyAI
{
    enum AI_State { Approach, Attack }
    [SerializeField] AI_State state;
    Vector3 playerPos;
    [SerializeField] bool moveSide;
    [SerializeField] NavMeshAgent agent;

    protected override void Awake()
    {
        base.Awake();
        state = AI_State.Approach;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemy.enemyData.MoveSpeed;
        agent.stoppingDistance = enemy.enemyData.Range * 0.9f;
    }

    protected override IEnumerator StateRoutine()
    {
        while (this)
        {
            if (enemy && enemy.alive)
            {
                state = StateCheck();
            }
            yield return null;
        }
    }

    protected override IEnumerator BehaviorRoutine()
    {
        while (this)
        {
            if (!enemy.isStunned && enemy.alive)
            {
                switch (state)
                {
                    case AI_State.Approach:
                        ApproachMove();
                        break;
                    case AI_State.Attack:
                        AttackMove();
                        break;
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }

    AI_State StateCheck()
    {
        playerPos = playerTransform.position + Vector3.up;
        float distance = Vector3.Distance(playerPos, enemy.EnemyPos);

        if (distance <= enemy.enemyData.Range)
        {
            if(state == AI_State.Approach)
                enemy.StartAttack();
            return AI_State.Attack;
        }

        if (state == AI_State.Attack)
        {
            agent.SetDestination(playerPos);
            enemy.StopAttack();
        }
        return AI_State.Approach;
    }

    void ApproachMove()
    {
        transform.LookAt(agent.nextPosition);
        transform.Translate(enemy.enemyData.MoveSpeed * Time.deltaTime * (playerPos - enemy.EnemyPos).normalized, Space.World);
    }

    void AttackMove()
    {
        transform.LookAt(playerPos);
        if (moveSide)
        {
            if (!enemy.TranslateGradually(transform.right, enemy.enemyData.MoveSpeed * 0.5f * Time.deltaTime))
                moveSide = !moveSide;
        }
        else
        {
            if (!enemy.TranslateGradually(-transform.right, enemy.enemyData.MoveSpeed * 0.5f * Time.deltaTime))
                moveSide = !moveSide;
        }
    }

    void OnDrawGizmos()
    {
        if (enemy.onGizmo)
        {

        }
    }
}
