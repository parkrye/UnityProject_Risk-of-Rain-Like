using UnityEngine;

[CreateAssetMenu(fileName = "Warrior_Action2A", menuName = "Data/Skill/Warrior/Action2A")]
public class Warrior_Action2A : Skill
{
    public override bool Active(bool isPressed)
    {
        if (coolCheck && isPressed)
        {
            hero.playerDataModel.animator.SetTrigger("Action2");
            hero.playerDataModel.animator.SetBool("Guard", true);

            coolCheck = false;

            return true;
        }
        else
        {
            hero.playerDataModel.animator.SetBool("Guard", false);
        }
        return false;
    }
}