using System.Collections;
using UnityEngine;

/// <summary>
/// 참격 날리기
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action1B", menuName = "Data/Skill/Warrior/Action1B")]
public class Warrior_Action1B : Skill, ICriticable
{
    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);

            GameObject slash = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/Slash"), true);
            slash.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            slash.GetComponent<Bolt>().Shot(hero.playerDataModel.playerAction.lookAtTransform.position, param[0] * modifier, 0.05f);

            CoolCheck = false;

            return true;
        }
        return false;
    }
}
