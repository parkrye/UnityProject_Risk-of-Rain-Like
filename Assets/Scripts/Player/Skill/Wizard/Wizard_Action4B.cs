using UnityEngine;

/// <summary>
/// 자이로볼
/// </summary>
[CreateAssetMenu(fileName = "Wizard_Action4B", menuName = "Data/Skill/Wizard/Action4B")]
public class Wizard_Action4B : Skill, ICriticable
{
    [SerializeField] float drawPower, drawRange;
    [SerializeField] ParticleSystem magicEffect;
    [SerializeField] GyroBolt gyroBoltPrefab;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.attackSource.Play();

            GameManager.Resource.Instantiate(magicEffect, hero.playerDataModel.playerTransform.position, Quaternion.identity, hero.playerDataModel.playerTransform, true);

            GyroBolt gyroBolt = GameManager.Resource.Instantiate(gyroBoltPrefab, true);
            gyroBolt.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            gyroBolt.Shot(hero.playerDataModel.playerAction.lookAtTransform.position, param[0] * modifier, 0.5f, drawPower, drawRange);

            CoolCheck = false;
            return true;
        }
        return false;
    }
}
