using UnityEngine;

/// <summary>
/// Ω∫≈« ¿Ãµø
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action3A", menuName = "Data/Skill/Archer/Action3A")]
public class Archer_Action3A : Skill
{
    public float stepDistance;

    void Awake()
    {
        SkillIcon = GameManager.Resource.Load<Icon>("Icon/Skill_Archer3A").sprite;
    }

    public override bool Active(bool isPressed)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[2]);

            Vector3 stepVec = hero.playerDataModel.playerMovement.moveDir;
            if (stepVec.magnitude == 0f)
                stepVec.z = 1f;

            RaycastHit hit;
            if (Physics.Raycast(hero.playerDataModel.transform.position + hero.playerDataModel.transform.up, (hero.playerDataModel.transform.right * stepVec.x + hero.playerDataModel.transform.forward * stepVec.z), out hit, stepDistance, LayerMask.GetMask("Ground")))
            {
                hero.playerDataModel.transform.position = hit.point;
            }
            else
            {
                hero.playerDataModel.transform.position += hero.playerDataModel.transform.right * stepVec.x + hero.playerDataModel.transform.forward * stepVec.z * stepDistance;
            }

            CoolCheck = false;

            return true;
        }
        return false;
    }
}
