using UnityEngine;

/// <summary>
/// 단발 사격
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action1A", menuName = "Data/Skill/Archer/Action1A")]
public class Archer_Action1A : Skill, ICriticable
{
    [SerializeField] Arrow arrow;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.attackSource.Play();

            Arrow arrowAttack = GameManager.Resource.Instantiate(arrow, true);
            arrowAttack.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            arrowAttack.Shot(hero.playerDataModel.playerAction.lookAtTransform.position, param[0] * modifier);

            CoolCheck = false;

            return true;
        }
        return false;
    }
}

