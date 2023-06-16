using UnityEngine;

/// <summary>
/// 자동 포탑 설치
/// </summary>
[CreateAssetMenu(fileName = "Wizard_Action4A", menuName = "Data/Skill/Wizard/Action4A")]
public class Wizard_Action4A : Skill
{
    public float skillRange, skillTerm;

    public override bool Active(bool isPressed)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger("Action4");

            RaycastHit hit;
            if (Physics.Raycast(hero.playerDataModel.playerAction.lookFromTransform.position, (hero.playerDataModel.playerAction.lookAtTransform.position - hero.playerDataModel.playerAction.lookFromTransform.position).normalized, out hit, skillRange, LayerMask.GetMask("Ground")))
            {
                GameObject tower = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/Tower"), hit.point, Quaternion.identity, true);
                tower.GetComponent<Tower>().SetTower(hero.playerDataModel.attackDamage * modifier, skillTerm, skillRange);
                CoolCheck = false;
            }

            return true;
        }
        return false;
    }
}
