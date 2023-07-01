using UnityEngine;

/// <summary>
/// 단발 사격
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action1A", menuName = "Data/Skill/Archer/Action1A")]
public class Archer_Action1A : Skill, ICriticable
{
    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.attackSource.Play();

            GameObject arrow = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/Arrow"), true);
            arrow.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            arrow.GetComponent<Arrow>().Shot(hero.playerDataModel.playerAction.lookAtTransform.position, param[0] * modifier);

            CoolCheck = false;

            return true;
        }
        return false;
    }
}

