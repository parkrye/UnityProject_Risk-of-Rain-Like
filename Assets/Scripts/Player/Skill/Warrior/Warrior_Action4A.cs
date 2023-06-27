using System.Collections;
using UnityEngine;

/// <summary>
/// 점프 강타
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action4A", menuName = "Data/Skill/Warrior/Action4A")]
public class Warrior_Action4A : Skill, IEnumeratable, ICriticable
{
    public float dashPower, skillRange;
    float damage;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            damage = param[0];
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);

            CoolCheck = false;

            return true;
        }
        return false;
    }

    public IEnumerator enumerator()
    {
        hero.playerDataModel.controllable = false;
        hero.playerDataModel.playerMovement.dirModifier += (hero.playerDataModel.playerTransform.forward + Vector3.up * 0.5f) * dashPower;

        yield return new WaitForSeconds(1f / hero.playerDataModel.TimeScale);

        ParticleSystem effect = GameManager.Resource.Instantiate(GameManager.Resource.Load<ParticleSystem>("Particle/Explosion"), hero.playerDataModel.playerTransform.position, Quaternion.identity, true);
        GameManager.Resource.Destroy(effect.gameObject, 2f);

        Collider[] colliders = Physics.OverlapSphere(hero.playerDataModel.playerTransform.position, skillRange);
        foreach (Collider collider in colliders)
        {
            IHitable hittable = collider.GetComponent<IHitable>();
            hittable?.Hit(damage * modifier);
        }

        yield return new WaitForSeconds(0.5f / hero.playerDataModel.TimeScale);

        hero.playerDataModel.controllable = true;
    }
}
