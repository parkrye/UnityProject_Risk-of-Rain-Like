using UnityEngine;

/// <summary>
/// 방패 올리기
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action2A", menuName = "Data/Skill/Warrior/Action2A")]
public class Warrior_Action2A : Skill
{
    public override bool Active(bool isPressed)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.playerDataModel.animator.SetBool("Guard", true);
            hero.playerDataModel.Buff(3, modifier);

            CoolCheck = false;

            return true;
        }
        else
        {
            hero.playerDataModel.animator.SetBool("Guard", false);
            hero.playerDataModel.Buff(3, 1 / modifier);
        }
        return false;
    }
}