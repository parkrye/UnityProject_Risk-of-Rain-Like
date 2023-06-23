using UnityEngine;

/// <summary>
/// 얼음 탄을 발사
/// </summary>
[CreateAssetMenu(fileName = "Wizard_Action1A", menuName = "Data/Skill/Wizard/Action1A")]
public class Wizard_Action1A : Skill
{
    void Awake()
    {
        SkillIcon = GameManager.Resource.Load<Icon>("Icon/Skill_Wizard1A").sprite;
    }

    public override bool Active(bool isPressed)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[0]);

            GameObject energyBolt = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/EnergyBolt"), true);
            energyBolt.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            energyBolt.GetComponent<Bolt>().Shot(hero.playerDataModel.playerAction.lookAtTransform.position, hero.playerDataModel.attackDamage * modifier);

            CoolCheck = false;

            return true;
        }
        return false;
    }
}
