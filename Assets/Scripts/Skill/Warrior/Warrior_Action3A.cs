using UnityEngine;

[CreateAssetMenu(fileName = "Warrior_Action3A", menuName = "Data/Skill/Warrior/Action3A")]
public class Warrior_Action3A : Skill
{
    public float dashPower;

    public override bool Active(bool isPressed)
    {
        if (coolCheck && isPressed)
        {
            hero.playerDataModel.animator.SetTrigger("Action3");

            Vector3 dashVec = hero.playerDataModel.playerMovement.moveDir;
            if (dashVec.magnitude == 0f)
                dashVec.z = 1f;

            hero.playerDataModel.rb.AddForce((dashVec.x * hero.playerDataModel.transform.right) + (dashVec.z * hero.playerDataModel.transform.forward) * dashPower, ForceMode.Impulse);

            coolCheck = false;

            return true;
        }
        return false;
    }
}
