using UnityEngine;

/// <summary>
/// ¾óÀ½Åº ¹ß»ç
/// </summary>
[CreateAssetMenu(fileName = "Wizard_Action1C", menuName = "Data/Skill/Wizard/Action1C")]
public class Wizard_Action1C : Skill, ICriticable
{
    public float slowTime, slowModifier;
    [SerializeField] ParticleSystem magicEffect;
    [SerializeField] SlowEnergyBolt bolt;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.attackSource.Play();

            GameManager.Resource.Instantiate(magicEffect, hero.playerDataModel.playerTransform.position, Quaternion.identity, hero.playerDataModel.playerTransform, true);

            SlowEnergyBolt slowEnergyBolt = GameManager.Resource.Instantiate(bolt, true);
            slowEnergyBolt.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            slowEnergyBolt.Shot(hero.playerDataModel.playerAction.lookAtTransform.position, param[0] * modifier, 0f, slowTime, slowModifier);

            CoolCheck = false;

            return true;
        }
        return false;
    }
}