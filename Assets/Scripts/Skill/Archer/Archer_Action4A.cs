using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Archer_Action4A", menuName = "Data/Skill/Archer/Action4A")]
public class Archer_Action4A : Skill
{
    public override bool Active(bool isPressed)
    {
        if (coolCheck && isPressed)
        {
            hero.playerDataModel.animator.SetTrigger("Action4");

            GameObject arrow = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/BigArrow"), true);
            arrow.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            arrow.transform.LookAt(hero.playerDataModel.playerCamera.lookAtTransform.position);
            arrow.GetComponent<Arrow>().Shot(100, 1f, 0.5f);

            coolCheck = false;

            return true;
        }
        return false;
    }
}
