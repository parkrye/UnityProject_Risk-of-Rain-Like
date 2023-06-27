using UnityEngine;

/// <summary>
/// Ω∫≈« ¿Ãµø
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action3A", menuName = "Data/Skill/Archer/Action3A")]
public class Archer_Action3A : Skill
{
    public float stepDistance;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);

            Vector3 stepVec = hero.playerDataModel.playerMovement.moveDir;
            if (stepVec.magnitude == 0f)
                stepVec.z = 1f;

            RaycastHit hit;
            if (Physics.Raycast(hero.playerDataModel.playerTransform.position + hero.playerDataModel.playerTransform.up, (hero.playerDataModel.playerTransform.right * stepVec.x + hero.playerDataModel.playerTransform.forward * stepVec.z), out hit, stepDistance, LayerMask.GetMask("Ground")))
            {
                hero.playerDataModel.playerTransform.position = hit.point;
            }
            else
            {
                hero.playerDataModel.playerTransform.position += hero.playerDataModel.playerTransform.right * stepVec.x + hero.playerDataModel.playerTransform.forward * stepVec.z * stepDistance;
            }

            CoolCheck = false;

            return true;
        }
        return false;
    }
}
