using System.Collections;
using UnityEngine;

/// <summary>
/// È­»ìºñ
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action4B", menuName = "Data/Skill/Archer/Action4B")]
public class Archer_Action4B : Skill, ICriticable
{
    [SerializeField] float skillRange;
    RaycastHit hit;
    float damage;
    GameObject arrowShower;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            damage = param[0] * modifier;
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            GameObject arrow = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/FlagArrow"), true);
            arrow.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            arrow.GetComponent<FlagArrow>().OnTriggerEnterEvent.AddListener(ShotArrowShower);
            arrow.GetComponent<FlagArrow>().Shot(hero.playerDataModel.playerAction.lookAtTransform.position, damage);
            CoolCheck = false;
            return true;
        }
        return false;
    }

    void ShotArrowShower(Transform transform)
    {
        arrowShower = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/ArrowShower"), true);
        arrowShower.GetComponent<ArrowShower>().Shot(transform, damage);
    }
}