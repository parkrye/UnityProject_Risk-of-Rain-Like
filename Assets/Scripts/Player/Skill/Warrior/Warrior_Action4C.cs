using System.Collections;
using UnityEngine;

/// <summary>
/// ±¤È­
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action4C", menuName = "Data/Skill/Warrior/Action4C")]
public class Warrior_Action4C : Skill, IEnumeratable
{
    public float skillTime;
    public float ReverseModifier;
    [SerializeField] ParticleSystem blood;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.powerupSource.Play();

            CoolCheck = false;

            return true;
        }
        return false;
    }

    public IEnumerator enumerator()
    {
        ParticleSystem effect = GameManager.Resource.Instantiate(blood, hero.playerDataModel.playerTransform.position, Quaternion.identity, hero.playerDataModel.playerTransform, true);
        hero.playerDataModel.playerSystem.Buff(0, modifier);
        hero.playerDataModel.playerSystem.Buff(1, modifier);
        hero.playerDataModel.playerSystem.Buff(2, modifier);
        hero.playerDataModel.playerSystem.Buff(3, modifier);
        hero.playerDataModel.playerSystem.Buff(4, modifier);
        yield return new WaitForSeconds(skillTime);
        hero.playerDataModel.playerSystem.Buff(0, ReverseModifier);
        hero.playerDataModel.playerSystem.Buff(1, ReverseModifier);
        hero.playerDataModel.playerSystem.Buff(2, ReverseModifier);
        hero.playerDataModel.playerSystem.Buff(3, ReverseModifier);
        hero.playerDataModel.playerSystem.Buff(4, ReverseModifier);
        GameManager.Resource.Destroy(effect);
    }
}
