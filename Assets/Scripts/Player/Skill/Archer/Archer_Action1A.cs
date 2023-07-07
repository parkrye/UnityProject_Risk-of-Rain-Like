using UnityEngine;
using System.Collections;

/// <summary>
/// 단발 사격
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action1A", menuName = "Data/Skill/Archer/Action1A")]
public class Archer_Action1A : Skill, ICriticable
{
    [SerializeField] Arrow arrow;   // 화살

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)  // 버튼을 누른 시점에
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);        // 애니메이터 실행
            hero.attackSource.Play();                                               // 소리 출력

            Arrow arrowAttack = GameManager.Resource.Instantiate(arrow, true);      // 화살을 생성하고
            arrowAttack.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
                                                                                    // 화살을 공격 위치로 옮기고
            arrowAttack.Shot(hero.playerDataModel.playerAction.lookAtTransform.position, param[0] * modifier);
                                                                                    // 화살을 발사

            CoolCheck = false;  // 쿨타임 체크

            return true;
        }
        return false;
    }
}

