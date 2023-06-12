using UnityEngine;

[CreateAssetMenu(fileName = "Wizard_Action3A", menuName = "Data/Skill/Wizard/Action3A")]
public class Wizard_Action3A : Skill
{
    public float teleportDistance;

    public override bool Active(bool isPressed)
    {
        if (coolCheck && isPressed)
        {
            hero.playerDataModel.animator.SetTrigger("Action3");

            Vector3 teleportVec = hero.playerDataModel.playerMovement.moveDir;
            if(teleportVec.magnitude == 0f)
                teleportVec.z = 1f;

            RaycastHit hit;
            if (Physics.Raycast(hero.playerDataModel.transform.position + hero.playerDataModel.transform.up, (hero.playerDataModel.transform.right * teleportVec.x + hero.playerDataModel.transform.forward * teleportVec.z), out hit, teleportDistance, LayerMask.GetMask("Ground")))
            {
                hero.playerDataModel.transform.position = hit.point;
            }
            else
            {
                hero.playerDataModel.transform.position += hero.playerDataModel.transform.right * teleportVec.x + hero.playerDataModel.transform.forward * teleportVec.z * teleportDistance;
            }

            coolCheck = false;

            return true;
        }
        return false;
    }
}
