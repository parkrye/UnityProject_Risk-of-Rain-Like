using System.Collections;
using UnityEngine;

/// <summary>
/// Æø¹ß È­»ì
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action4A", menuName = "Data/Skill/Archer/Action4A")]
public class Archer_Action4A : Skill, IEnumeratable
{
    public float skillTime;

    void Awake()
    {
        SkillIcon = GameManager.Resource.Load<Icon>("Icon/Skill_Archer4A").sprite;
    }

    public override bool Active(bool isPressed)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[3]);

            GameObject bombArrow = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/BombArrow"), true);
            bombArrow.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            bombArrow.GetComponent<BombArrow>().Shot(hero.playerDataModel.playerAction.lookAtTransform.position, hero.playerDataModel.attackDamage * modifier, 0.5f);

            CoolCheck = false;

            return true;
        }
        return false;
    }

    public IEnumerator enumerator()
    {
        hero.playerDataModel.controlleable = false;
        yield return new WaitForSeconds(skillTime);
        hero.playerDataModel.controlleable = true;
    }
}
