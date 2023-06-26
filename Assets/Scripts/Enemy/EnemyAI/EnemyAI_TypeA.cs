using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 공중 몬스터 AI
/// 접근 => ( 우회 접근 ) => 공격
/// </summary>
public class EnemyAI_TypeA : EnemyAI
{
    enum AI_State { Approach, Bypass, Attack }
    [SerializeField] AI_State state;
    Stack<Vector3> bypass;
    Vector3 prevPlayerPosition;
    [SerializeField] bool onGizmo;

    protected override void Awake()
    {
        base.Awake();
        state = AI_State.Approach;
        bypass = new Stack<Vector3>();
    }

    void Update()
    {
        state = StateCheck();
    }

    void FixedUpdate()
    {
        if (!enemy.isStunned)
        {
            switch (state)
            {
                case AI_State.Approach:
                    Approach();
                    break;
                case AI_State.Bypass:
                    ByPass();
                    break;
                case AI_State.Attack:
                    Attack();
                    break;
            }
        }
    }

    AI_State StateCheck()
    {
        RaycastHit hit;
        if(Physics.SphereCast(transform.position + Vector3.up, enemy.enemyData.Size, (player.position - transform.position).normalized, out hit, Vector3.Distance(player.position, transform.position) - enemy.enemyData.Size, LayerMask.GetMask("Ground")))
        {
            if (state == AI_State.Attack)
                enemy.StopAttack();
            if (state != AI_State.Bypass)
                StartCoroutine(FindBypass());
            return AI_State.Bypass;
        }

        if (Vector3.SqrMagnitude(player.position - transform.position) <= Mathf.Pow(enemy.enemyData.Range, 2))
        {
            if(state != AI_State.Attack)
                enemy.StartAttack();
            return AI_State.Attack;
        }
        else
        {
            if(state == AI_State.Attack)
                enemy.StopAttack();
            return AI_State.Approach;
        }
    }

    void Attack()
    {
        transform.LookAt(player.position + Vector3.up);
    }

    void Approach()
    {
        transform.LookAt(player.position + Vector3.up);
        transform.Translate(((player.position + Vector3.up) - (transform.position + Vector3.up)).normalized * enemy.enemyData.MoveSpeed * Time.deltaTime, Space.World);
    }

    void ByPass()
    {
        if (bypass.Count > 0)
        {
            transform.LookAt(bypass.Peek());
            transform.Translate((bypass.Peek() - (transform.position + Vector3.up)).normalized * enemy.enemyData.MoveSpeed * Time.deltaTime, Space.World);
            if(Vector3.SqrMagnitude((transform.position + Vector3.up) - bypass.Peek()) <= Mathf.Pow(enemy.enemyData.Size * 0.5f, 2))
                bypass.Pop();
        }
    }

    IEnumerator FindBypass()
    {
        transform.LookAt(player.position + Vector3.up);
        bypass = PathFinder.PathFindingForAerial(transform, player.position + Vector3.up, enemy.enemyData.Size);
        prevPlayerPosition = player.position;

        while (state == AI_State.Bypass)
        {
            if(Vector3.SqrMagnitude(player.position - prevPlayerPosition) > 9f || bypass.Count == 0)
            {
                transform.LookAt(player.position + Vector3.up);
                bypass = PathFinder.PathFindingForAerial(transform, player.position + Vector3.up, enemy.enemyData.Size);
                prevPlayerPosition = player.position;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void OnDrawGizmos()
    {
        if (onGizmo)
        {
            Gizmos.color = Color.green;
            if (state == AI_State.Bypass && bypass.Count > 0)
            {
                foreach (var bypass in bypass)
                {
                    Gizmos.DrawWireSphere(bypass, enemy.enemyData.Size);
                }
            }
        }
    }
}
