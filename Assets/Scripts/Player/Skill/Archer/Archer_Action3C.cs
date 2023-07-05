using System.Collections;
using UnityEngine;

/// <summary>
/// ¾ÞÄ¿ ÀÌµ¿
/// </summary>
[CreateAssetMenu(fileName = "Archer_Action3C", menuName = "Data/Skill/Archer/Action3C")]
public class Archer_Action3C : Skill, IEnumeratable, ICriticable
{
    [SerializeField] float anchorPower;
    [SerializeField] FlagArrow flag;
    Transform anchorTransform;
    bool anchorOn;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.attackSource.Play();

            FlagArrow arrow = GameManager.Resource.Instantiate(flag, true);
            arrow.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            arrow.OnTriggerEnterEvent.AddListener(AnchorDrag);
            arrow.Shot(hero.playerDataModel.playerAction.lookAtTransform.position, param[0] * modifier);
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
