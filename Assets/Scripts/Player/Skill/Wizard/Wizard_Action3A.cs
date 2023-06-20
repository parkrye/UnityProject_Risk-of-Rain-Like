using System.Collections;
using UnityEngine;

/// <summary>
/// 전방으로 텔레포트
/// </summary>
[CreateAssetMenu(fileName = "Wizard_Action3A", menuName = "Data/Skill/Wizard/Action3A")]
public class Wizard_Action3A : Skill, IEnumeratable
{
    public float teleportDistance, teleportCharge;
    public bool nowCharge;

    public override bool Active(bool isPressed)
    {
        if (nowCharge)
            nowCharge = false;

        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[2]);

            return true;
        }
        return false;
    }

    public IEnumerator enumerator()
    {
        teleportCharge = 0f;
        nowCharge = true;
        while (teleportCharge < 1f && nowCharge)
        {
            yield return new WaitForSeconds(0.01f);
            teleportCharge += 0.01f;
        }
        Teleport();
    }

    void Teleport()
    {
        RaycastHit hit;
        if (Physics.Raycast(hero.playerDataModel.transform.position + Vector3.up, hero.playerDataModel.playerAction.lookFromTransform.forward.normalized, out hit, teleportDistance * teleportCharge, LayerMask.GetMask("Ground")))
        {
            hero.playerDataModel.transform.position = hit.point;
        }
        else
        {
            hero.playerDataModel.transform.position += hero.playerDataModel.playerAction.lookFromTransform.forward * teleportDistance * teleportCharge;
        }
        CoolCheck = false;
        hero.playerDataModel.animator.SetTrigger("Teleport");
    }
}
