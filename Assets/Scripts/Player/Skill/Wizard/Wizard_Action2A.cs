using UnityEngine;

/// <summary>
/// 누르는 동안 불 발사
/// </summary>
[CreateAssetMenu(fileName = "Wizard_Action2A", menuName = "Data/Skill/Wizard/Action2A")]
public class Wizard_Action2A : Skill
{
    Flame flame;
    [SerializeField] ParticleSystem magicEffect;
    [SerializeField] Flame flamePrefab;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.playerDataModel.animator.SetBool("Casting", true);

            GameManager.Resource.Instantiate(magicEffect, hero.playerDataModel.playerTransform.position, Quaternion.identity, hero.playerDataModel.playerTransform, true);
            flame = GameManager.Resource.Instantiate(flamePrefab, hero.playerDataModel.playerAction.lookFromTransform, true);
            flame.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            flame.transform.LookAt(hero.playerDataModel.playerAction.lookAtTransform.position + Vector3.up);
            flame.StartFlame(hero.playerDataModel.AttackDamage * modifier);

            return true;
        }
        else
        {
            if (hero.playerDataModel.animator.GetBool("Casting"))
            {
                hero.playerDataModel.animator.SetBool("Casting", false);
                CoolCheck = false;
                flame?.StopFlame();
            }
            flame = null;
        }
        return false;
    }
}