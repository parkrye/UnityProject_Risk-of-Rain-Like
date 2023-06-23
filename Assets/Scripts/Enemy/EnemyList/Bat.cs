using Cinemachine.Utility;
using System.Collections;
using UnityEngine;

public class Bat : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        enemyData = GameManager.Resource.Load<EnemyData>("Enemy/Bat");
    }

    protected override IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (attack)
            {
                animator.SetTrigger("Attack");
                GameObject enemyBolt = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("EnemyAttack/EnemyBolt"), transform.position + Vector3.up, Quaternion.identity, true);
                enemyBolt.GetComponent<EnemyBolt>().Shot(GameManager.Data.Player.transform.position, damage);
                yield return new WaitForSeconds(enemyData.AttackSpeed);
            }
            else
            {
                yield return null;
            }
        }
    }
}
