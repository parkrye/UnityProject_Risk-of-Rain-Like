using System.Collections;
using UnityEngine;

/// <summary>
/// ���� ȭ��
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action4A", menuName = "Data/Skill/Archer/Action4A")]
public class Archer_Action4A : Skill, IEnumeratable, ICriticable
{
    public float skillTime;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.attackSource.Play();

            GameObject bombArrow = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/BombArrow"), true);
            bombArrow.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            bombArrow.GetComponent<BombArrow>().Shot(hero.playerDataModel.playerAction.lookAtTransform.position, param[0], 0.5f);

            CoolCheck = false;

            return true;
        }
        return false;
    }

    public IEnumerator enumerator()
    {
        hero.playerDataModel.controllable = false;
        yield return new WaitForSeconds(skillTime / hero.playerDataModel.TimeScale);
        hero.playerDataModel.controllable = true;
    }
}
