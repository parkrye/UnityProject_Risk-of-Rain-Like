using UnityEngine;

[CreateAssetMenu(fileName = "Archer_Action1A", menuName = "Data/Skill/Archer/Action1A")]
public class Archer_Action1A : Skill
{
    public override bool Active(bool isPressed)
    {
        if (coolCheck && isPressed)
        {
            hero.playerDataModel.animator.SetTrigger("Action1");

            GameObject arrow = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/Arrow"), true);
            arrow.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            arrow.transform.LookAt(hero.playerDataModel.playerAction.lookAtTransform.position);
            arrow.GetComponent<Arrow>().Shot(50, hero.playerDataModel.attackDamage * modifier);

            coolCheck = false;

            return true;
        }
        return false;
    }
}

