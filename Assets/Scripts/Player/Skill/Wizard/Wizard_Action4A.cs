using System.Collections;
using UnityEngine;

/// <summary>
/// 자동 포탑 설치
/// </summary>
[CreateAssetMenu(fileName = "Wizard_Action4A", menuName = "Data/Skill/Wizard/Action4A")]
public class Wizard_Action4A : Skill, IEnumeratable
{
    [SerializeField] float skillRange;
    RaycastHit hit;
    bool summon;
    GameObject tower;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            return true;
        }
        else
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            ParticleSystem effect = GameManager.Resource.Instantiate(GameManager.Resource.Load<ParticleSystem>("Particle/MagicEffect"), hero.playerDataModel.playerTransform.position, Quaternion.identity, hero.playerDataModel.playerTransform, true);
            tower.GetComponent<Tower>().SetTower(param[0] * modifier);
            CoolCheck = false;
            summon = false;
        }
        return false;
    }

    public IEnumerator enumerator()
    {
        summon = true;
        tower = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/Tower"), true);
        while (summon)
        {
            Physics.Raycast(hero.playerDataModel.playerAction.lookFromTransform.position, (hero.playerDataModel.playerAction.lookAtTransform.position - hero.playerDataModel.playerAction.lookFromTransform.position).normalized, out hit, skillRange, LayerMask.GetMask("Ground"));
            tower.transform.position = hit.point;
            yield return null;
        }
    }
}
