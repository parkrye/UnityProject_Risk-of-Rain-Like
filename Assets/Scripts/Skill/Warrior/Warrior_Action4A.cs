using UnityEngine;

[CreateAssetMenu(fileName = "Warrior_Action4A", menuName = "Data/Skill/Warrior/Action4A")]
public class Warrior_Action4A : Skill
{
    public override bool Active(bool isPressed)
    {
        if (coolCheck && isPressed)
        {
            GameManager.Data.Player.animator.SetTrigger("Action4");

            coolCheck = false;

            return true;
        }
        return false;
    }
}
