using System.Collections;
using UnityEngine;

/// <summary>
/// ȸ��
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action3C", menuName = "Data/Skill/Warrior/Action3C")]
public class Warrior_Action3C : Skill, IEnumeratable
{
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
        ParticleSystem effect = GameManager.Resource.Instantiate(GameManager.Resource.Load<ParticleSystem>("Particle/Sparkle"), hero.playerDataModel.playerTransform.position, Quaternion.identity, hero.playerDataModel.playerTransform, true);
        hero.playerDataModel.playerSystem.Buff(0, modifier);
        hero.playerDataModel.NOWHP += hero.playerDataModel.MAXHP / 5f;
        for(int i = 0; i < 100; i++)
        {
            hero.playerDataModel.NOWHP += hero.playerDataModel.MAXHP < 100f ? 0.1f : hero.playerDataModel.MAXHP / 1000f;
            yield return new WaitForSeconds(0.1f);
        }
        GameManager.Resource.Destroy(effect);
    }
}
