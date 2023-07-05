using System.Collections;
using UnityEngine;

/// <summary>
/// 유도탄을 3발 발사
/// </summary>
[CreateAssetMenu(fileName = "Wizard_Action1B", menuName = "Data/Skill/Wizard/Action1B")]
public class Wizard_Action1B : Skill, ICriticable, IEnumeratable
{
    float damage;
    [SerializeField] ParticleSystem magicEffect;
    [SerializeField] FollowBolt bolt;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.attackSource.Play();

            ParticleSystem effect = GameManager.Resource.Instantiate(magicEffect, hero.playerDataModel.playerTransform.position, Quaternion.identity, hero.playerDataModel.playerTransform, true);
            damage = param[0] * modifier;
            CoolCheck = false;

            return true;
        }
        return false;
    }

    public IEnumerator enumerator()
    {
        for (int i = 0; i < 3; i++)
        {
            FollowBolt followBolt = GameManager.Resource.Instantiate(bolt, true);
            followBolt.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            followBolt.Shot(hero.playerDataModel.playerAction.lookAtTransform.position, damage);
            yield return new WaitForSeconds(0.3f * hero.playerDataModel.ReverseTimeScale);
        }
    }
}
