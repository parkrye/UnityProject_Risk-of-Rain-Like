using UnityEngine;

[CreateAssetMenu(fileName = "Warrior_Action2A", menuName = "Data/Skill/Warrior/Action2A")]
public class Warrior_Action2A : Skill
{
    public override bool Active(bool isPressed)
    {
        if (coolCheck && isPressed)
        {
            GameManager.Data.Player.animator.SetTrigger("Action2");
            GameManager.Data.Player.animator.SetBool("Guard", true);

            coolCheck = false;

            return true;
        }
        else
        {
            GameManager.Data.Player.animator.SetBool("Guard", false);
        }
        return false;
    }
}