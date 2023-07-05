using UnityEngine;

/// <summary>
/// ºÎ¸Þ¶û ¹ß»ç
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action2C", menuName = "Data/Skill/Archer/Action2C")]
public class Archer_Action2C : Skill, ICriticable
{
    [SerializeField] Boomerang boomerang;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.attackSource.Play();

            Boomerang boomerangAttack = GameManager.Resource.Instantiate(boomerang, true);
            boomerangAttack.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            boomerangAttack.Shot(hero.playerDataModel.playerTransform, hero.playerDataModel.playerAction.lookAtTransform.position, param[0] * modifier);

            CoolCheck = false;

            return true;
        }
        return false;
    }
}