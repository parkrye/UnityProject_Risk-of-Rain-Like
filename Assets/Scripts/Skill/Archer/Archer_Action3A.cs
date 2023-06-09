using UnityEngine;

[CreateAssetMenu(fileName = "Archer_Action3A", menuName = "Data/Skill/Archer/Action3A")]
public class Archer_Action3A : Skill
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
