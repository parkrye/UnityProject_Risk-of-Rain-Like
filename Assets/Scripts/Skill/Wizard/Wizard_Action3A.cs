using UnityEngine;

[CreateAssetMenu(fileName = "Wizard_Action3A", menuName = "Data/Skill/Wizard/Action3A")]
public class Wizard_Action3A : Skill
{
    public override bool Active(bool isPressed)
    {
        if (coolCheck && isPressed)
        {
            GameManager.Data.Player.animator.SetTrigger("Action3");

            coolCheck = false;

            return true;
        }
        return false;
    }
}
