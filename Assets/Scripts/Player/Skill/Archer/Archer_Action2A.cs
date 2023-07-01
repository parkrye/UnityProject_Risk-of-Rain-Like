using UnityEngine;

/// <summary>
/// 3¹ß »ç°Ý
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action2A", menuName = "Data/Skill/Archer/Action2A")]
public class Archer_Action2A : Skill, ICriticable
{
    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.attackSource.Play();

            for (int i = -1; i <= 1; i++)
            {
                GameObject arrow = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/Arrow"), true);
                arrow.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
                arrow.GetComponent<Arrow>().Shot(hero.playerDataModel.playerAction.lookAtTransform.position + hero.playerDataModel.playerAction.lookAtTransform.right * i, param[0] * modifier);
            }

            CoolCheck = false;

            return true;
        }
        return false;
    }
}
