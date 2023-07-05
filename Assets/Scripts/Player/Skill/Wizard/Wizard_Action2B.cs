using UnityEngine;

/// <summary>
/// 충격탄을 발사
/// </summary>
[CreateAssetMenu(fileName = "Wizard_Action2B", menuName = "Data/Skill/Wizard/Action2B")]
public class Wizard_Action2B : Skill, ICriticable
{
    public float shockRange, shockTime;
    [SerializeField] ParticleSystem magicEffect;
    [SerializeField] ShockBolt ShockBoltPrefab;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.attackSource.Play();

            GameManager.Resource.Instantiate(magicEffect, hero.playerDataModel.playerTransform.position, Quaternion.identity, hero.playerDataModel.playerTransform, true);

            ShockBolt shockBolt = GameManager.Resource.Instantiate(ShockBoltPrefab, true);
            shockBolt.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            shockBolt.Shot(hero.playerDataModel.playerAction.lookAtTransform.position, param[0] * modifier, 0f, shockRange, shockTime);

            CoolCheck = false;

            return true;
        }
        return false;
    }
}
