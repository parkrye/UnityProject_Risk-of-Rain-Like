using UnityEngine;

[CreateAssetMenu(fileName = "Archer_Action4A", menuName = "Data/Skill/Archer/Action4A")]
public class Archer_Action4A : Skill
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
