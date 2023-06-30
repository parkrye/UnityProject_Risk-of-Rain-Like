using System.Collections;
using UnityEngine;

/// <summary>
/// 패턴 1: 근접 공격
/// 패턴 2: 휴식
/// </summary>
public class Golem : Boss
{
    IEnumerator healCoroutine;

    protected override void Awake()
    {
        enemyData = GameManager.Resource.Load<EnemyData>("Boss/Golem");
        healCoroutine = HealRoutine();
        base.Awake();
    }

    public override void ChangeToClose()
    {
        animator.SetBool("Heal", false);
        StopCoroutine(healCoroutine);
    }

    public override void ChangeToFar()
    {
        StartCoroutine(healCoroutine);
    }

    protected override IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (attack)
            {
                animator.SetTrigger("Attack");

                Collider[] colliders = Physics.OverlapSphere(attackTransform.position, enemyData.floatdatas[1]);
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("Player"))
                    {
                        IHitable hittable = collider.GetComponent<IHitable>();
                        hittable?.Hit(damage, 0f);
                    }
                }
                yield return new WaitForSeconds(enemyData.AttackSpeed);
                animator.SetBool("SerialAttack", !animator.GetBool("SerialAttack"));
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator HealRoutine()
    {
        animator.SetBool("Heal", true);
        while (true)
        {
            HP += enemyData.floatdatas[0] * Time.deltaTime;
            yield return null;
        }
    }
}
