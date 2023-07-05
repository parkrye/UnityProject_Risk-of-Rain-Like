using UnityEngine;

/// <summary>
/// ¼ö·ùÅº ÅõÃ´
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action2B", menuName = "Data/Skill/Archer/Action2B")]
public class Archer_Action2B : Skill, ICriticable
{
    [SerializeField] BombArrow bombArrow;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.attackSource.Play();

            BombArrow bomb = GameManager.Resource.Instantiate(bombArrow, true);
            bomb.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            bomb.Shot(hero.playerDataModel.playerAction.lookAtTransform.position, param[0] * modifier);

            CoolCheck = false;

            return true;
        }
        return false;
    }
}