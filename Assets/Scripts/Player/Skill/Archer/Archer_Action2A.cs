using UnityEngine;

/// <summary>
/// 3�� ���
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action2A", menuName = "Data/Skill/Archer/Action2A")]
public class Archer_Action2A : Skill
{
    void Awake()
    {
        SkillIcon = GameManager.Resource.Load<Icon>("Icon/Skill_Archer2A").sprite;
    }

    public override bool Active(bool isPressed)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[1]);

            for(int i = -1; i <= 1; i++)
            {
                GameObject arrow = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/Arrow"), true);
                arrow.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
                arrow.GetComponent<Arrow>().Shot(hero.playerDataModel.playerAction.lookAtTransform.position + hero.playerDataModel.playerAction.lookAtTransform.right * i, hero.playerDataModel.attackDamage * modifier);
            }

            CoolCheck = false;

            return true;
        }
        return false;
    }
}
