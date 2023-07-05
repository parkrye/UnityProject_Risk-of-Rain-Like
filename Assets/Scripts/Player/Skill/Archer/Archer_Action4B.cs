using System.Collections;
using UnityEngine;

/// <summary>
/// È­»ìºñ
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action4B", menuName = "Data/Skill/Archer/Action4B")]
public class Archer_Action4B : Skill, ICriticable
{
    [SerializeField] float skillRange;
    [SerializeField] FlagArrow flag;
    [SerializeField] ArrowShower shower;
    float damage;
    ArrowShower arrowShower;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            damage = param[0] * modifier;

            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.attackSource.Play();

            FlagArrow arrow = GameManager.Resource.Instantiate(flag, true);
            arrow.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            arrow.OnTriggerEnterEvent.AddListener(ShotArrowShower);
            arrow.Shot(hero.playerDataModel.playerAction.lookAtTransform.position, damage);
            CoolCheck = false;
            return true;
        }
        return false;
    }

    void ShotArrowShower(Transform target)
    {
        arrowShower = GameManager.Resource.Instantiate(shower, true);
        arrowShower.Shot(target, damage);
    }
}