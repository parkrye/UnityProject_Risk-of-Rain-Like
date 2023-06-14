using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Archer_Action4A", menuName = "Data/Skill/Archer/Action4A")]
public class Archer_Action4A : Skill
{
    public override bool Active(bool isPressed)
    {
        if (coolCheck && isPressed)
        {
            hero.playerDataModel.controlleable = false;
            hero.playerDataModel.animator.SetTrigger("Action4");

            GameObject bombArrow = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/BombArrow"), true);
            bombArrow.transform.position = hero.playerDataModel.playerAction.AttackTransform.position + hero.playerDataModel.transform.up;
            bombArrow.transform.LookAt(hero.playerDataModel.playerAction.lookAtTransform.position);
            bombArrow.GetComponent<BombArrow>().Shot(50, hero.playerDataModel.attackDamage * modifier, 0.5f);

            coolCheck = false;

            return true;
        }
        return false;
    }

    public void AnimationEnd()
    {
        hero.playerDataModel.controlleable = true;
    }
}
