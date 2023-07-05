using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 유도 화살
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action1C", menuName = "Data/Skill/Archer/Action1C")]
public class Archer_Action1C : Skill, ICriticable
{
    [SerializeField] FollowBolt followBolt;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.attackSource.Play();

            FollowBolt arrow = GameManager.Resource.Instantiate(followBolt, true);
            arrow.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            arrow.Shot(hero.playerDataModel.playerAction.lookAtTransform.position, param[0] * modifier);

            CoolCheck = false;

            return true;
        }
        return false;
    }
}
