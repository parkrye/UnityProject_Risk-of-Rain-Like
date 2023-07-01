using UnityEngine;

/// <summary>
/// ¼ö·ùÅº ÅõÃ´
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action2B", menuName = "Data/Skill/Archer/Action2B")]
public class Archer_Action2B : Skill, ICriticable
{
    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.attackSource.Play();

            GameObject bomb = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/Bomb"), true);
            bomb.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            bomb.GetComponent<BombArrow>().Shot(hero.playerDataModel.playerAction.lookAtTransform.position, param[0] * modifier);

            CoolCheck = false;

            return true;
        }
        return false;
    }
}