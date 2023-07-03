using UnityEngine;

/// <summary>
/// ¾óÀ½Åº ¹ß»ç
/// </summary>
[CreateAssetMenu(fileName = "Wizard_Action1C", menuName = "Data/Skill/Wizard/Action1C")]
public class Wizard_Action1C : Skill, ICriticable
{
    public float slowTime, slowModifier;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.attackSource.Play();

            ParticleSystem effect = GameManager.Resource.Instantiate(GameManager.Resource.Load<ParticleSystem>("Particle/MagicEffect"), hero.playerDataModel.playerTransform.position, Quaternion.identity, hero.playerDataModel.playerTransform, true);
            GameManager.Resource.Destroy(effect.gameObject, 2f);

            GameObject slowEnergyBolt = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/SlowEnergyBolt"), true);
            slowEnergyBolt.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            slowEnergyBolt.GetComponent<SlowEnergyBolt>().Shot(hero.playerDataModel.playerAction.lookAtTransform.position, param[0] * modifier, 0f, slowTime, slowModifier);

            CoolCheck = false;

            return true;
        }
        return false;
    }
}