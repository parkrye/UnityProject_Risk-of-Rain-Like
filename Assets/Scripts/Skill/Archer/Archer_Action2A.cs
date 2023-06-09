using UnityEngine;

[CreateAssetMenu(fileName = "Archer_Action2A", menuName = "Data/Skill/Archer/Action2A")]
public class Archer_Action2A : Skill
{
    public override bool Active(bool isPressed)
    {
        if (coolCheck && isPressed)
        {
            GameManager.Data.Player.animator.SetTrigger("Action2");

            coolCheck = false;

            return true;
        }
        return false;
    }
}
