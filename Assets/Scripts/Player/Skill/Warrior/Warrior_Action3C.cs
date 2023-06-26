using System.Collections;
using UnityEngine;

/// <summary>
/// È¸º¹
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action3C", menuName = "Data/Skill/Warrior/Action3C")]
public class Warrior_Action3C : Skill, IEnumeratable
{
    public override bool Active(bool isPressed)
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
        hero.playerDataModel.NOWHP += hero.playerDataModel.MAXHP / 5f;
        for(int i = 0; i < 100; i++)
        {
            hero.playerDataModel.NOWHP += hero.playerDataModel.MAXHP < 100f ? 0.1f : hero.playerDataModel.MAXHP / 1000f;
            yield return new WaitForSeconds(0.1f);
        }

    }
}
