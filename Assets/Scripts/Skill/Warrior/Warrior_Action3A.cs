using UnityEngine;

[CreateAssetMenu(fileName = "Warrior_Action3A", menuName = "Data/Skill/Warrior/Action3A")]
public class Warrior_Action3A : Skill
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
