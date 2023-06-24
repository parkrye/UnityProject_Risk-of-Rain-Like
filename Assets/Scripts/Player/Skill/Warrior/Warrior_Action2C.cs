using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ¹ßÂ÷±â
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action2C", menuName = "Data/Skill/Warrior/Action2C")]
public class Warrior_Action2C : Skill, IEnumeratable
{
    public float knockbackDistance;
    public override bool Active(bool isPressed)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);

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
                mazable?.KnockBack(knockbackDistance, hero.playerDataModel.transform);
            }
        }
    }
}