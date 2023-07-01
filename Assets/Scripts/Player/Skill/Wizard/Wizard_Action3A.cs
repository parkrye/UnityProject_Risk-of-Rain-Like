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

    public override bool Active(bool isPressed, params float[] param)
    {
        if (nowCharge)
            nowCharge = false;

        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);

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
            yield return new WaitForSeconds(0.01f / hero.playerDataModel.TimeScale);
            teleportCharge += 0.01f;
        }
        Teleport();
    }

    void Teleport()
    {
        RaycastHit hit;
        if (Physics.Raycast(hero.playerDataModel.playerTransform.position + Vector3.up, hero.playerDataModel.playerAction.lookFromTransform.forward.normalized, out hit, teleportDistance * teleportCharge, LayerMask.GetMask("Ground")))
        {
            hero.playerDataModel.playerTransform.position = hit.point;
        }
        else
        {
            hero.playerDataModel.playerTransform.position += hero.playerDataModel.playerAction.lookFromTransform.forward * teleportDistance * teleportCharge;
        }
        CoolCheck = false;
        hero.playerDataModel.animator.SetTrigger("Teleport");
        ParticleSystem effect = GameManager.Resource.Instantiate(GameManager.Resource.Load<ParticleSystem>("Particle/MagicEffect"), hero.playerDataModel.playerTransform.position, Quaternion.identity, true);
        GameManager.Resource.Destroy(effect.gameObject, 2f);
    }
}
