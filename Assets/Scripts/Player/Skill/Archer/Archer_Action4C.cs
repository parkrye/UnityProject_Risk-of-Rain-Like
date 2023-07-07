using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 강한 대기 화살
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action4C", menuName = "Data/Skill/Archer/Action4C")]
public class Archer_Action4C : Skill, ICriticable, IEnumeratable
{
    [SerializeField] int count, limit;
    Stack<int> stack = new Stack<int>();
    [SerializeField] float chargeTime;
    [SerializeField] Arrow arrow;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            if(count < limit)
            {
                stack.Push(count++);

                hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
                hero.attackSource.Play();

                Arrow ArrowAttack = GameManager.Resource.Instantiate(arrow, true);
                ArrowAttack.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
                ArrowAttack.Shot(hero.playerDataModel.playerAction.lookAtTransform.position, param[0] * modifier);

                CoolCheck = false;
            }

            return true;
        }
        return false;
    }

    public IEnumerator Enumerator()
    {
        yield return new WaitForSeconds(chargeTime * hero.playerDataModel.ReverseTimeScale);
        if(stack.Count == count && count > 0)
            count = stack.Pop();
    }
}
