using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 공중 몬스터 AI
/// 접근 => ( 우회 접근 ) => 공격
/// </summary>
public class EnemyAI_TypeA : EnemyAI
{
    enum AI_State { Approach, Bypass, Atttack }
    [SerializeField] AI_State state;
    Stack<Vector3> bypass;
    bool needToBypass;
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
                case AI_State.Atttack:
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
            enemy.StopAttack();
            return AI_State.Bypass;
        }

        needToBypass = false;

        if (Vector3.SqrMagnitude(player.position - transform.position) <= Mathf.Pow(enemy.enemyData.Range, 2))
        {
            return AI_State.Atttack;
        }
        else
        {
            enemy.StopAttack();
            return AI_State.Approach;
        }
    }

    void Attack()
    {
        transform.LookAt(player.position + Vector3.up);
        enemy.StartAttack();
    }

    void Approach()
    {
        transform.LookAt(player.position + Vector3.up);
        transform.Translate(((player.position + Vector3.up) - (transform.position + Vector3.up)).normalized * enemy.enemyData.MoveSpeed * Time.deltaTime, Space.World);
    }

    void ByPass()
    {
        if (!needToBypass)
        {
            StartCoroutine(FindBypass());
        }

        if (needToBypass)
        {
            if(bypass.Count > 0)
            {
                transform.LookAt(bypass.Peek());
                transform.Translate((bypass.Peek() - (transform.position + Vector3.up)).normalized * enemy.enemyData.MoveSpeed * Time.deltaTime, Space.World);
                if (Vector3.Distance(transform.position + Vector3.up, bypass.Peek()) <= 0.5f)
                    bypass.Pop();
            }
        }
    }

    IEnumerator FindBypass()
    {
        transform.LookAt(player.position + Vector3.up);
        bypass = PathFinder.PathFindingForAerial(transform, player.position + Vector3.up, enemy.enemyData.Size);
        prevPlayerPosition = player.position;
        needToBypass = true;

        while (needToBypass)
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
            if (bypass.Count > 0)
            {
                foreach (var bypass in bypass)
                {
                    Gizmos.DrawWireSphere(bypass, enemy.enemyData.Size);
                }
            }
        }
    }
}
