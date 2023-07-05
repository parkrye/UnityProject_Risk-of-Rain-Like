using UnityEngine;

/// <summary>
/// µ¶ ´Ë
/// </summary>
[CreateAssetMenu(fileName = "Wizard_Action4C", menuName = "Data/Skill/Wizard/Action4C")]
public class Wizard_Action4C : Skill, ICriticable
{
    [SerializeField] float skillRange, skillTime;
    [SerializeField] ParticleSystem magicEffect;
    [SerializeField] PoisonSwamp poisonSwampPrefab;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);

            GameManager.Resource.Instantiate(magicEffect, hero.playerDataModel.playerTransform.position, Quaternion.identity, hero.playerDataModel.playerTransform, true);

            PoisonSwamp poisonSwamp = GameManager.Resource.Instantiate(poisonSwampPrefab, true);
            poisonSwamp.transform.position = hero.playerDataModel.transform.position;
            poisonSwamp.SpwanSwamp(param[0] * modifier, skillRange, skillTime);

            CoolCheck = false;
            return true;
        }
        return false;
    }
}
