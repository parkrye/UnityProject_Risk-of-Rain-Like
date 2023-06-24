using System.Collections;
using UnityEngine;
/// <summary>
/// 주변베기
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action1C", menuName = "Data/Skill/Warrior/Action1C")]
public class Warrior_Action1C : Skill, IEnumeratable
{
    public float skillRangeModifier;

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

        Collider[] colliders = Physics.OverlapSphere(hero.playerDataModel.transform.position + Vector3.up, hero.playerDataModel.playerAction.closeAttackRange * skillRangeModifier);
        foreach (Collider collider in colliders)
        {
            if (!collider.CompareTag("Player"))
            {
                IHitable hittable = collider.GetComponent<IHitable>();
                hittable?.Hit(hero.playerDataModel.AttackDamage * modifier);
            }
        }
    }
}