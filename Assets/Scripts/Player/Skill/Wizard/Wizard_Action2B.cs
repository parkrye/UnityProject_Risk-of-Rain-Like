using UnityEngine;

/// <summary>
/// 충격탄을 발사
/// </summary>
[CreateAssetMenu(fileName = "Wizard_Action2B", menuName = "Data/Skill/Wizard/Action2B")]
public class Wizard_Action2B : Skill, ICriticable
{
    public float shockRange, shockTime;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.attackSource.Play();

            ParticleSystem effect = GameManager.Resource.Instantiate(GameManager.Resource.Load<ParticleSystem>("Particle/MagicEffect"), hero.playerDataModel.playerTransform.position, Quaternion.identity, hero.playerDataModel.playerTransform, true);

            GameObject shockBolt = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/ShockBolt"), true);
            shockBolt.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            shockBolt.GetComponent<ShockBolt>().Shot(hero.playerDataModel.playerAction.lookAtTransform.position, param[0] * modifier, 0f, shockRange, shockTime);

            CoolCheck = false;

            return true;
        }
        return false;
    }
}
