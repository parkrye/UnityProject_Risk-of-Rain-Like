using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ¹ßÂ÷±â
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action2C", menuName = "Data/Skill/Warrior/Action2C")]
public class Warrior_Action2C : Skill, IEnumeratable, ICriticable
{
    public float knockbackDistance;
    float damage;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            damage = param[0];
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
                hittable?.Hit(damage * modifier);

                IMazable mazable = collider.GetComponent<IMazable>();
                mazable?.KnockBack(knockbackDistance, hero.playerDataModel.playerTransform);
            }
        }
    }
}