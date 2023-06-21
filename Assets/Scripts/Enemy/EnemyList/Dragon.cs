using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
    bool attacking;

    protected override void Awake()
    {
        base.Awake();
        enemyData = GameManager.Resource.Load<EnemyData>("Enemy/Dragon");
    }

    protected override IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (attack)
            {
                if (!attacking)
                {
                    animator.SetTrigger("Attack");
                    GameObject enemyFlame = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("EnemyAttack/EnemyFlame"), transform.position, Quaternion.identity, transform, true);
                    enemyFlame.transform.LookAt(transform.forward);
                    enemyFlame.GetComponent<EnemyFlame>().damage = damage;
                    attacking = true;
                }
                yield return new WaitForSeconds(enemyData.Speed);
            }
            else
            {
                attacking = false;
                yield return null;
            }
        }
    }
}
