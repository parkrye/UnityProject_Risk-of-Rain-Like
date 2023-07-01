using System.Collections;
using UnityEngine;

/// <summary>
/// 이속, 점프 버프
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action3B", menuName = "Data/Skill/Warrior/Action3B")]
public class Warrior_Action3B : Skill, IEnumeratable
{
    public float skillTime;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);

            CoolCheck = false;

            return true;
        }
        return false;
    }

    public IEnumerator enumerator()
    {
        ParticleSystem effect = GameManager.Resource.Instantiate(GameManager.Resource.Load<ParticleSystem>("Particle/_Burf1"), hero.playerDataModel.playerTransform.position, Quaternion.identity, hero.playerDataModel.playerTransform, true);
        hero.playerDataModel.playerSystem.Buff(0, modifier);
        hero.playerDataModel.playerSystem.Buff(1, modifier);
        yield return new WaitForSeconds(skillTime);
        hero.playerDataModel.playerSystem.Buff(0, 1f / modifier);
        hero.playerDataModel.playerSystem.Buff(1, 1f / modifier);
        GameManager.Resource.Destroy(effect.gameObject);
    }
}
