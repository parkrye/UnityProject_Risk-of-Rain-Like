using System.Collections;
using UnityEngine;
/// <summary>
/// �ֺ�����
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action1C", menuName = "Data/Skill/Warrior/Action1C")]
public class Warrior_Action1C : Skill, IEnumeratable, ICriticable
{
    public float skillRangeModifier;
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

        Collider[] colliders = Physics.OverlapSphere(hero.playerDataModel.playerTransform.position + Vector3.up, hero.playerDataModel.playerAction.closeAttackRange * skillRangeModifier);
        foreach (Collider collider in colliders)
        {
            if (!collider.CompareTag("Player"))
            {
                IHitable hittable = collider.GetComponent<IHitable>();
                hittable?.Hit(damage * modifier, 0f);
            }
        }
    }
}