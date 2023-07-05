using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 자동 포탑 설치
/// </summary>
[CreateAssetMenu(fileName = "Wizard_Action4A", menuName = "Data/Skill/Wizard/Action4A")]
public class Wizard_Action4A : Skill, IEnumeratable
{
    [SerializeField] float skillRange;
    [SerializeField] ParticleSystem magicEffect;
    [SerializeField] Tower towerPrefab;
    RaycastHit hit;
    bool summon;
    Tower tower;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            return true;
        }
        else
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            GameManager.Resource.Instantiate(magicEffect, hero.playerDataModel.playerTransform.position, Quaternion.identity, hero.playerDataModel.playerTransform, true);
            tower.SetTower(param[0] * modifier);
            GameManager.Resource.Instantiate<MinimapMarker>("Marker/MinimapMarker_Tower", true).StartFollowing(tower.transform);
            CoolCheck = false;
            summon = false;
        }
        return false;
    }

    public IEnumerator enumerator()
    {
        summon = true;
        tower = GameManager.Resource.Instantiate(towerPrefab, true);
        while (summon)
        {
            Physics.Raycast(hero.playerDataModel.playerAction.lookFromTransform.position, (hero.playerDataModel.playerAction.lookAtTransform.position - hero.playerDataModel.playerAction.lookFromTransform.position).normalized, out hit, skillRange, LayerMask.GetMask("Ground"));
            tower.transform.position = hit.point;
            yield return null;
        }
    }
}
