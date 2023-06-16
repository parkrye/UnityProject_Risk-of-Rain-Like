using UnityEngine;

/// <summary>
/// 방패 올리기
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action2A", menuName = "Data/Skill/Warrior/Action2A")]
public class Warrior_Action2A : Skill
{
    float prevArmor;

    public override bool Active(bool isPressed)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger("Action2");
            hero.playerDataModel.animator.SetBool("Guard", true);
            prevArmor = hero.playerDataModel.armorPoint;
            hero.playerDataModel.armorPoint *= 0.5f;

            CoolCheck = false;

            return true;
        }
        else
        {
            hero.playerDataModel.animator.SetBool("Guard", false);
            hero.playerDataModel.armorPoint = prevArmor;
        }
        return false;
    }
}