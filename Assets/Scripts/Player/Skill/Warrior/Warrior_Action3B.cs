using System.Collections;
using UnityEngine;

/// <summary>
/// 이속, 점프 버프
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action3B", menuName = "Data/Skill/Warrior/Action3B")]
public class Warrior_Action3B : Skill, IEnumeratable
{
    public float skillTime;

    public override bool Active(bool isPressed, params float[] param)
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
        hero.playerDataModel.Buff(0, modifier);
        hero.playerDataModel.Buff(1, modifier);
        yield return new WaitForSeconds(skillTime);
        hero.playerDataModel.Buff(0, 1 / modifier);
        hero.playerDataModel.Buff(1, 1 / modifier);
    }
}
