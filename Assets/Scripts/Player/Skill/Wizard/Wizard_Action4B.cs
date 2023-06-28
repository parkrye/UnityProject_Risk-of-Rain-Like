using UnityEngine;

/// <summary>
/// 자이로볼
/// </summary>
[CreateAssetMenu(fileName = "Wizard_Action4B", menuName = "Data/Skill/Wizard/Action4B")]
public class Wizard_Action4B : Skill, ICriticable
{
    [SerializeField] float drawPower, drawRange;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);

            GameObject gyroBolt = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/GyroBolt"), true);
            gyroBolt.transform.position = hero.playerDataModel.playerAction.AttackTransform.position;
            gyroBolt.GetComponent<GyroBolt>().Shot(hero.playerDataModel.playerAction.lookAtTransform.position, param[0] * modifier, 0.5f, drawPower, drawRange);

            CoolCheck = false;
            return true;
        }
        return false;
    }
}
