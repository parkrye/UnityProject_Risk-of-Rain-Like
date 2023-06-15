using UnityEngine;

/// <summary>
/// 3¹ß »ç°Ý
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action2A", menuName = "Data/Skill/Archer/Action2A")]
public class Archer_Action2A : Skill
{
    public override bool Active(bool isPressed)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger("Action2");

            for(int i = -1; i <= 1; i++)
            {
                GameObject arrow = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/Arrow"), true);
                arrow.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
                arrow.transform.LookAt(hero.playerDataModel.playerAction.lookAtTransform.position + hero.playerDataModel.playerAction.lookAtTransform.right * i);
                arrow.GetComponent<Arrow>().Shot(30, hero.playerDataModel.attackDamage * modifier);
            }

            coolCheck = false;

            return true;
        }
        return false;
    }
}
