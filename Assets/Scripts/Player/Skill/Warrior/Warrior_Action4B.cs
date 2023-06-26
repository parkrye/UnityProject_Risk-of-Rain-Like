using System.Collections;
using UnityEngine;

/// <summary>
/// 시간 가속
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action4B", menuName = "Data/Skill/Warrior/Action4B")]
public class Warrior_Action4B : Skill, IEnumeratable
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
        Time.timeScale = modifier;
        hero.playerDataModel.TimeScale = 1 / modifier;
        yield return new WaitForSeconds(skillTime / hero.playerDataModel.TimeScale);
        Time.timeScale = 1f;
        hero.playerDataModel.TimeScale = 1f;
    }
}
