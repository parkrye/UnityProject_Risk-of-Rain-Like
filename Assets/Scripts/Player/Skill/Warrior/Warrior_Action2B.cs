using System.Collections;
using UnityEngine;

/// <summary>
/// 방패 치기
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action2B", menuName = "Data/Skill/Warrior/Action2B")]
public class Warrior_Action2B : Skill, IEnumeratable
{
    public float stunTime;


    public override bool Active(bool isPressed)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            Debug.Log($"{actionNum}, {actionKeys[actionNum]}");

            CoolCheck = false;

            return true;
        }
        return false;
    }

    public IEnumerator enumerator()
    {
        yield return new WaitForSeconds(0.08f / hero.playerDataModel.TimeScale);

        Collider[] colliders = Physics.OverlapSphere(hero.playerDataModel.playerAction.lookFromTransform.position, hero.playerDataModel.playerAction.closeAttackRange);
        foreach (Collider collider in colliders)
        {
            if (!collider.CompareTag("Player"))
            {
                IHitable hittable = collider.GetComponent<IHitable>();
                hittable?.Hit(hero.playerDataModel.AttackDamage * modifier);

                IMazable mazable = collider.GetComponent<IMazable>();
                mazable?.Stuned(stunTime);
            }
        }
    }
}
