using UnityEngine;

/// <summary>
/// ±¸¸£±â
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action3A", menuName = "Data/Skill/Warrior/Action3A")]
public class Warrior_Action3A : Skill
{
    public override bool Active(bool isPressed)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger("Action3");

            Vector3 dashVec = hero.playerDataModel.playerMovement.moveDir;
            if (dashVec.magnitude == 0f)
                dashVec.z = 1f;

            coolCheck = false;

            return true;
        }
        return false;
    }
}
