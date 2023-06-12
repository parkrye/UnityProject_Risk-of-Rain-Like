using UnityEngine;

[CreateAssetMenu(fileName = "Archer_Action1A", menuName = "Data/Skill/Archer/Action1A")]
public class Archer_Action1A : Skill
{
    public override bool Active(bool isPressed)
    {
        if (coolCheck && isPressed)
        {
            hero.playerDataModel.animator.SetTrigger("Action1");

            GameObject arrow = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/Throwing"), true);
            arrow.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            arrow.transform.LookAt(hero.playerDataModel.playerCamera.lookAtTransform.position);
            arrow.GetComponent<Arrow>().Shot();

            coolCheck = false;

            return true;
        }
        return false;
    }
}

