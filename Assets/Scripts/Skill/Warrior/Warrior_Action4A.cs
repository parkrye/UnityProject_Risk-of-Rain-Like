using System.Collections;
using UnityEngine;

/// <summary>
/// 점프 강타
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action4A", menuName = "Data/Skill/Warrior/Action4A")]
public class Warrior_Action4A : Skill, IEnumeratable
{
    public float dashPower, skillRange;

    public override bool Active(bool isPressed)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger("Action4");

            coolCheck = false;

            return true;
        }
        return false;
    }

    public IEnumerator enumerator()
    {
        hero.playerDataModel.controlleable = false;
        hero.playerDataModel.rb.AddForce((hero.playerDataModel.transform.forward + hero.playerDataModel.transform.up * 0.5f) * dashPower, ForceMode.Impulse);

        yield return new WaitForSeconds(1f);

        ParticleSystem effect = GameManager.Resource.Instantiate(GameManager.Resource.Load<ParticleSystem>("Particle/Explosion"), hero.playerDataModel.transform.position, Quaternion.identity, true);
        GameManager.Resource.Destroy(effect.gameObject, 2f);

        Collider[] colliders = Physics.OverlapSphere(hero.playerDataModel.transform.position, skillRange);
        foreach (Collider collider in colliders)
        {
            IHitable hittable = collider.GetComponent<IHitable>();
            hittable?.Hit(hero.playerDataModel.attackDamage * modifier);
        }

        yield return new WaitForSeconds(1f);

        hero.playerDataModel.controlleable = true;
    }
}
