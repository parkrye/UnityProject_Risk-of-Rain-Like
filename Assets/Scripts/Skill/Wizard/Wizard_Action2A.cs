using UnityEngine;

[CreateAssetMenu(fileName = "Wizard_Action2A", menuName = "Data/Skill/Wizard/Action2A")]
public class Wizard_Action2A : Skill
{
    public override bool Active(bool isPressed)
    {
        if (coolCheck && isPressed)
        {
            hero.playerDataModel.animator.SetTrigger("Action2");
            hero.playerDataModel.animator.SetBool("Casting", true);

            coolCheck = false;

            return true;
        }
        else
        {
            hero.playerDataModel.animator.SetBool("Casting", false);
        }
        return false;
    }
}