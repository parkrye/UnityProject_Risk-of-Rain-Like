using System.Collections;
using UnityEngine;

/// <summary>
/// Æòº£±â
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action1A", menuName = "Data/Skill/Warrior/Action1A")]
public class Warrior_Action1A : Skill, IEnumeratable
{
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
            if(!collider.CompareTag("Player"))
            {
                IHitable hittable = collider.GetComponent<IHitable>();
                hittable?.Hit(hero.playerDataModel.AttackDamage * modifier);
            }
        }
    }
}
