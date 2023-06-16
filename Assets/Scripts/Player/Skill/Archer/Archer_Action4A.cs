using System.Collections;
using UnityEngine;

/// <summary>
/// Æø¹ß È­»ì
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action4A", menuName = "Data/Skill/Archer/Action4A")]
public class Archer_Action4A : Skill, IEnumeratable
{
    public float skillTime;

    public override bool Active(bool isPressed)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger("Action4");

            GameObject bombArrow = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/BombArrow"), true);
            bombArrow.transform.position = hero.playerDataModel.playerAction.AttackTransform.position + hero.playerDataModel.transform.up;
            bombArrow.transform.LookAt(hero.playerDataModel.playerAction.lookAtTransform.position);
            bombArrow.GetComponent<BombArrow>().Shot(40, hero.playerDataModel.attackDamage * modifier, 0.5f);

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
