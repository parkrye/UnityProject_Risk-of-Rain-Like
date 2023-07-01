using System.Collections;
using UnityEngine;

/// <summary>
/// ¸ÅÀÇ ´«
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action3B", menuName = "Data/Skill/Archer/Action3B")]
public class Archer_Action3B : Skill, IEnumeratable
{
    public float skillTime;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            ParticleSystem effect = GameManager.Resource.Instantiate(GameManager.Resource.Load<ParticleSystem>("Particle/Swift"), hero.playerDataModel.playerTransform.position, Quaternion.identity, true);
            GameManager.Resource.Destroy(effect.gameObject, 0.8f);
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);

            CoolCheck = false;

            return true;
        }
        return false;
    }

    public IEnumerator enumerator()
    {
        hero.playerDataModel.playerSystem.Buff(3, modifier);
        hero.playerDataModel.playerSystem.Buff(4, modifier);
        yield return new WaitForSeconds(skillTime);
        hero.playerDataModel.playerSystem.Buff(3, 1 / modifier);
        hero.playerDataModel.playerSystem.Buff(4, 1 / modifier);
    }
}