using UnityEngine;

/// <summary>
/// µ¶Åº ¹ß»ç
/// </summary>
[CreateAssetMenu(fileName = "Wizard_Action2C", menuName = "Data/Skill/Wizard/Action2C")]
public class Wizard_Action2C : Skill, ICriticable
{
    public float poisonTime;
    [SerializeField] ParticleSystem magicEffect;
    [SerializeField] PoisonBolt poisonBoltPrefab;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.attackSource.Play();

            GameManager.Resource.Instantiate(magicEffect, hero.playerDataModel.playerTransform.position, Quaternion.identity, hero.playerDataModel.playerTransform, true);

            PoisonBolt poisonBolt = GameManager.Resource.Instantiate(poisonBoltPrefab, true);
            poisonBolt.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            poisonBolt.Shot(hero.playerDataModel.playerAction.lookAtTransform.position, param[0] * modifier, 0f, poisonTime);

            CoolCheck = false;

            return true;
        }
        return false;
    }
}
