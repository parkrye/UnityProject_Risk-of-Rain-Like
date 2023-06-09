using UnityEngine;

[CreateAssetMenu(fileName = "Warrior_Action1A", menuName = "Data/Skill/Warrior/Action1A")]
public class Warrior_Action1A : Skill
{
    public override bool Active(bool isPressed)
    {
        if (coolCheck && isPressed)
        {
            GameManager.Data.Player.animator.SetTrigger("Action1");

            coolCheck = false;

            return true;
        }
        return false;
    }
}
