using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ¾ÞÄ¿ ÀÌµ¿
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action3C", menuName = "Data/Skill/Archer/Action3C")]
public class Archer_Action3C : Skill, IEnumeratable, ICriticable
{
    [SerializeField] float anchorPower;
    Transform anchorTransform;
    bool anchorOn;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            GameObject arrow = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/FlagArrow"), true);
            arrow.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            arrow.GetComponent<FlagArrow>().OnTriggerEnterEvent.AddListener(AnchorDrag);
            arrow.GetComponent<FlagArrow>().Shot(hero.playerDataModel.playerAction.lookAtTransform.position, param[0] * modifier);
            CoolCheck = false;
            return true;
        }
        return false;
    }

    void AnchorDrag(Transform _transform)
    {
        anchorTransform = _transform;
        anchorOn = true;
    }

    public IEnumerator enumerator()
    {
        while (!anchorOn)
        {
            yield return null;
        }

        hero.playerDataModel.playerMovement.dirModifier += (anchorTransform.position - hero.playerDataModel.playerTransform.position).normalized * anchorPower;

        anchorOn = false;
    }
}
