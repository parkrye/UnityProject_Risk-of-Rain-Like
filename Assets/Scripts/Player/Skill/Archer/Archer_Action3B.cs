using System.Collections;
using UnityEngine;

/// <summary>
/// ¸ÅÀÇ ´«
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action3B", menuName = "Data/Skill/Archer/Action3B")]
public class Archer_Action3B : Skill, IEnumeratable
{
    public float skillTime;
    public float reverseModifier;
    [SerializeField] ParticleSystem swift;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.powerupSource.Play();

            GameManager.Resource.Instantiate(swift, hero.playerDataModel.playerTransform.position, Quaternion.identity, true);

            CoolCheck = false;

            return true;
        }
        return false;
    }

    public IEnumerator Enumerator()
    {
        hero.playerDataModel.playerSystem.Buff(3, modifier);
        hero.playerDataModel.playerSystem.Buff(4, modifier);
        yield return new WaitForSeconds(skillTime * hero.playerDataModel.ReverseTimeScale);
        hero.playerDataModel.playerSystem.Buff(3, reverseModifier);
        hero.playerDataModel.playerSystem.Buff(4, reverseModifier);
    }
}