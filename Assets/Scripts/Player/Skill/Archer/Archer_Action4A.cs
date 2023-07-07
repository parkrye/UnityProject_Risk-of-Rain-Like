using System.Collections;
using UnityEngine;

/// <summary>
/// Æø¹ß È­»ì
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action4A", menuName = "Data/Skill/Archer/Action4A")]
public class Archer_Action4A : Skill, IEnumeratable, ICriticable
{
    public float skillTime;
    [SerializeField] BombArrow bomb;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.attackSource.Play();

            BombArrow bombArrow = GameManager.Resource.Instantiate(bomb, true);
            bombArrow.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            bombArrow.Shot(hero.playerDataModel.playerAction.lookAtTransform.position, param[0], 0.5f);

            CoolCheck = false;

            return true;
        }
        return false;
    }

    public IEnumerator Enumerator()
    {
        hero.playerDataModel.controllable = false;
        yield return new WaitForSeconds(skillTime * hero.playerDataModel.ReverseTimeScale);
        hero.playerDataModel.controllable = true;
    }
}
