using UnityEngine;

/// <summary>
/// 방패 올리기
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action2A", menuName = "Data/Skill/Warrior/Action2A")]
public class Warrior_Action2A : Skill, IDamageSubscriber
{
    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.playerDataModel.animator.SetBool("Guard", true);
            hero.playerDataModel.playerSystem.AddDamageSubscriber(this);

            CoolCheck = false;

            return true;
        }
        else
        {
            hero.playerDataModel.animator.SetBool("Guard", false);
            hero.playerDataModel.playerSystem.RemoveDamageSubscriber(this);
        }
        return false;
    }

    public float ModifiyDamage(float _damage)
    {
        return _damage * 0.5f;
    }
}