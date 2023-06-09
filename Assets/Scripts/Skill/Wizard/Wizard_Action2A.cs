using UnityEngine;

[CreateAssetMenu(fileName = "Wizard_Action2A", menuName = "Data/Skill/Wizard/Action2A")]
public class Wizard_Action2A : Skill
{
    public override bool Active(bool isPressed)
    {
        if (coolCheck && isPressed)
        {
            GameManager.Data.Player.animator.SetTrigger("Action2");
            GameManager.Data.Player.animator.SetBool("Casting", true);

            coolCheck = false;

            return true;
        }
        else
        {
            GameManager.Data.Player.animator.SetBool("Casting", false);
        }
        return false;
    }
}