using System.Collections;
using UnityEngine;

public class Golem : Boss
{
    protected override void Awake()
    {
        enemyData = GameManager.Resource.Load<EnemyData>("Boss/Golem");
        base.Awake();
    }

    public override void ChangeToClose()
    {
        animator.SetBool("Heal", false);
        StopCoroutine(HealRoutine());
    }

    public override void ChangeToFar()
    {
        StartCoroutine(HealRoutine());
    }

    protected override IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (attack)
            {
                animator.SetTrigger("Attack");
                yield return new WaitForSeconds(enemyData.AttackSpeed);
                animator.SetBool("SerialAttack", !animator.GetBool("SerialAttack"));
            }
        }
    }

    IEnumerator HealRoutine()
    {
        animator.SetBool("Heal", true);
        while (true)
        {
            hp += enemyData.floatdatas[0] * Time.deltaTime;
            yield return null;
        }
    }
}
