using System.Collections;
using UnityEngine;

public class Dragon : Enemy
{
    [SerializeField] Transform mouse;
    GameObject enemyFlame;
    bool attacking;

    protected override void Awake()
    {
        base.Awake();
        attacking = false;
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
                    StartCoroutine(Breath());
                }
                yield return null;
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator Breath()
    {
        Debug.Log($"{name} Start Breath!");
        attacking = true;
        enemyFlame = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("EnemyAttack/EnemyFlame"), transform, true);
        enemyFlame.GetComponent<EnemyFlame>().Shot(damage, mouse);
        enemyFlame.transform.LookAt(mouse.position + transform.forward);
        yield return new WaitForSeconds(enemyData.floatdatas[0]);
        GameManager.Resource.Destroy(enemyFlame);
        Debug.Log($"{name} End Breath!");
        yield return new WaitForSeconds(enemyData.floatdatas[1]);
        Debug.Log($"{name} Ready Breath!");
        attacking = false;
    }
}
