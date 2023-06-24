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
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);

            for(int i = -1; i <= 1; i++)
            {
                GameObject arrow = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/Arrow"), true);
                arrow.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
                arrow.GetComponent<Arrow>().Shot(hero.playerDataModel.playerAction.lookAtTransform.position + hero.playerDataModel.playerAction.lookAtTransform.right * i, hero.playerDataModel.AttackDamage * modifier);
            }

            CoolCheck = false;

            return true;
        }
        return false;
    }
}
