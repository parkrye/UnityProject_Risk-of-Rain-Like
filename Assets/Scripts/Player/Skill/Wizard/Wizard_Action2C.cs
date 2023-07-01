using UnityEngine;

/// <summary>
/// µ¶Åº ¹ß»ç
/// </summary>
[CreateAssetMenu(fileName = "Wizard_Action2C", menuName = "Data/Skill/Wizard/Action2C")]
public class Wizard_Action2C : Skill, ICriticable
{
    public float poisonTime;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.attackSource.Play();

            ParticleSystem effect = GameManager.Resource.Instantiate(GameManager.Resource.Load<ParticleSystem>("Particle/MagicEffect"), hero.playerDataModel.playerTransform.position, Quaternion.identity, true);
            GameManager.Resource.Destroy(effect.gameObject, 2f);

            GameObject poisonBolt = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/PoisonBolt"), true);
            poisonBolt.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            poisonBolt.GetComponent<PoisonBolt>().Shot(hero.playerDataModel.playerAction.lookAtTransform.position, param[0] * modifier, 0f, poisonTime);

            CoolCheck = false;

            return true;
        }
        return false;
    }
}
