using UnityEngine;

/// <summary>
/// µ¶ ´Ë
/// </summary>
[CreateAssetMenu(fileName = "Wizard_Action4C", menuName = "Data/Skill/Wizard/Action4C")]
public class Wizard_Action4C : Skill, ICriticable
{
    [SerializeField] float skillRange, skillTime;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);

            GameObject gyroBolt = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/PoisonSwamp"), true);
            gyroBolt.transform.position = hero.playerDataModel.transform.position;
            gyroBolt.GetComponent<PoisonSwamp>().SpwanSwamp(param[0] * modifier, skillRange, skillTime);

            CoolCheck = false;
            return true;
        }
        return false;
    }
}
