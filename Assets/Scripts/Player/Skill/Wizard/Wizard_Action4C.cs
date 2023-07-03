using UnityEngine;

/// <summary>
/// µ¶ ´Ë
/// </summary>
[CreateAssetMenu(fileName = "Wizard_Action4C", menuName = "Data/Skill/Wizard/Action4C")]
public class Wizard_Action4C : Skill, ICriticable
{
    [SerializeField] float skillRange, skillTime;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);

            ParticleSystem effect = GameManager.Resource.Instantiate(GameManager.Resource.Load<ParticleSystem>("Particle/MagicEffect"), hero.playerDataModel.playerTransform.position, Quaternion.identity, hero.playerDataModel.playerTransform, true);
            GameManager.Resource.Destroy(effect.gameObject, 2f);

            GameObject poisonSwamp = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/PoisonSwamp"), true);
            poisonSwamp.transform.position = hero.playerDataModel.transform.position;
            poisonSwamp.GetComponent<PoisonSwamp>().SpwanSwamp(param[0] * modifier, skillRange, skillTime);

            CoolCheck = false;
            return true;
        }
        return false;
    }
}
