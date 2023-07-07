using System.Collections;
using UnityEngine;

/// <summary>
/// 시간 가속
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action4B", menuName = "Data/Skill/Warrior/Action4B")]
public class Warrior_Action4B : Skill, IEnumeratable
{
    public float skillTime;
    public float reverseModifier;
    [SerializeField] ParticleSystem electricity;

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

    public IEnumerator Enumerator()
    {
        ParticleSystem effect = GameManager.Resource.Instantiate(electricity, hero.playerDataModel.playerTransform.position + Vector3.up, Quaternion.identity, hero.playerDataModel.playerTransform, true);
        Time.timeScale = modifier;
        hero.playerDataModel.TimeScale = reverseModifier;
        yield return new WaitForSeconds(skillTime * hero.playerDataModel.ReverseTimeScale);
        Time.timeScale = 1f;
        hero.playerDataModel.TimeScale = 1f;
        GameManager.Resource.Destroy(effect);
    }
}
