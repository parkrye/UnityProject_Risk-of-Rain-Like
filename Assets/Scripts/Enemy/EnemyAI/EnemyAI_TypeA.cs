using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 공중 몬스터 AI
/// 우회 접근  => 공격
/// </summary>
public class EnemyAI_TypeA : EnemyAI
{
    enum AI_State { Bypass, Atttack }
    [SerializeField] AI_State state;
    Stack<Vector3> bypass;
    bool needToBypass;
    Vector3 prevPlayerPosition;

    protected override void Awake()
    {
        base.Awake();
        state = AI_State.Bypass;
        bypass = new Stack<Vector3>();
    }

    void Update()
    {
        state = StateCheck();
        switch (state)
        {
            case AI_State.Bypass:
                ByPass();
                break;
            case AI_State.Atttack:
                Attack();
                break;
        }
    }

    AI_State StateCheck()
    {
        /*
        RaycastHit hit;
        if(Physics.SphereCast(transform.position + Vector3.up, enemy.enemyData.Size, (player.position - transform.position).normalized, out hit, Vector3.Distance(player.position, transform.position) - 1f, LayerMask.GetMask("Ground")))
         */

        if (Vector3.Distance(player.position + Vector3.up, transform.position + Vector3.up) <= enemy.enemyData.Range)
        {
            needToBypass = false;
            return AI_State.Atttack;
        }
        else
        {
            enemy.StopAttack();
            return AI_State.Bypass;
        }
    }

    void Attack()
    {
        transform.LookAt(player.position + Vector3.up);
        enemy.StartAttack();
    }

    void ByPass()
    {
        if (!needToBypass)
        {
            needToBypass = true;
            StartCoroutine(FindBypass());
        }

        if (needToBypass)
        {
            if(bypass.Count == 0)
                bypass = PathFinder.PathFindingForAerial(transform.position + Vector3.up, player.position + Vector3.up);
            transform.Translate((bypass.Peek() - (transform.position + Vector3.up)).normalized * enemy.enemyData.MoveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position + Vector3.up, bypass.Peek()) <= 0.5f)
                bypass.Pop();
        }
    }

    IEnumerator FindBypass()
    {
        while (needToBypass)
        {
            if(Vector3.Distance(prevPlayerPosition, GameManager.Data.Player.transform.position) > 3f)
            {
                bypass = PathFinder.PathFindingForAerial(transform.position + Vector3.up, player.position + Vector3.up);
                prevPlayerPosition = GameManager.Data.Player.transform.position;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(bypass.Count > 0)
        {
            foreach (var bypass in bypass)
            {
                Gizmos.DrawWireSphere(bypass, 0.5f);
            }
        }
    }
}
