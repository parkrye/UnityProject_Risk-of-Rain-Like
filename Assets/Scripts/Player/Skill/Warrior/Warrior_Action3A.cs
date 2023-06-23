using System.Collections;
using UnityEngine;

/// <summary>
/// ±¸¸£±â
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action3A", menuName = "Data/Skill/Warrior/Action3A")]
public class Warrior_Action3A : Skill, IEnumeratable
{
    void Awake()
    {
        SkillIcon = GameManager.Resource.Load<Icon>("Icon/Skill_Warrior3A").sprite;
    }

    public override bool Active(bool isPressed)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[2]);

            Vector3 dashVec = hero.playerDataModel.playerMovement.moveDir;
            if (dashVec.magnitude == 0f)
                dashVec.z = 1f;
            CoolCheck = false;

            return true;
        }
        return false;
    }

    public IEnumerator enumerator()
    {
        yield return new WaitForSeconds(0.2f);
        hero.playerDataModel.dodgeDamage = true;
        yield return new WaitForSeconds(0.3f);
        hero.playerDataModel.dodgeDamage = false;
    }
}
