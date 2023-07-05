using UnityEngine;

/// <summary>
/// 3¹ß »ç°Ý
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action2A", menuName = "Data/Skill/Archer/Action2A")]
public class Archer_Action2A : Skill, ICriticable
{
    [SerializeField] Arrow arrow;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.attackSource.Play();

            for (int i = -1; i <= 1; i++)
            {
                Arrow arrowAttack = GameManager.Resource.Instantiate(arrow, true);
                arrowAttack.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
                arrowAttack.Shot(hero.playerDataModel.playerAction.lookAtTransform.position + hero.playerDataModel.playerAction.lookAtTransform.right * i, param[0] * modifier);
            }

            CoolCheck = false;

            return true;
        }
        return false;
    }
}
