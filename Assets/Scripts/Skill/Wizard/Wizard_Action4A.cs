using UnityEngine;

[CreateAssetMenu(fileName = "Wizard_Action4A", menuName = "Data/Skill/Wizard/Action4A")]
public class Wizard_Action4A : Skill
{
    public override bool Active(bool isPressed)
    {
        if (coolCheck && isPressed)
        {
            hero.playerDataModel.animator.SetTrigger("Action4");

            coolCheck = false;

            return true;
        }
        return false;
    }
}
