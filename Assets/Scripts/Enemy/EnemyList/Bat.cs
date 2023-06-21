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
                GameObject enemyBolt = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("EnemyAttack/EnemyBolt"), true);
                enemyBolt.transform.position = transform.position;
                enemyBolt.transform.LookAt(GameManager.Data.Player.transform.position);
                enemyBolt.GetComponent<EnemyBolt>().Shot(gameObject.GetComponent<Enemy>().damage);
                yield return new WaitForSeconds(enemyData.Speed);
            }
            else
            {
                yield return null;
            }
        }
    }
}
