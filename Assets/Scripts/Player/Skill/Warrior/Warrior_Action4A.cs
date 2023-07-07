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
    [SerializeField] ParticleSystem crack;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            damage = param[0];

            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            hero.attackSource.Play();

            CoolCheck = false;

            return true;
        }
        return false;
    }

    public IEnumerator Enumerator()
    {
        hero.playerDataModel.controllable = false;
        hero.playerDataModel.playerMovement.dirModifier += (hero.playerDataModel.playerTransform.forward + Vector3.up * 0.5f) * dashPower;

        yield return new WaitForSeconds(1f * hero.playerDataModel.ReverseTimeScale);

        ParticleSystem effect = GameManager.Resource.Instantiate(crack, hero.playerDataModel.playerTransform.position, Quaternion.identity, true);

        Collider[] colliders = Physics.OverlapSphere(hero.playerDataModel.playerTransform.position, skillRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (!colliders[i].CompareTag("Player"))
            {
                IHitable hittable = colliders[i].GetComponent<IHitable>();
                hittable?.Hit(damage * modifier, 0f);
            }
        }

        yield return new WaitForSeconds(0.5f * hero.playerDataModel.ReverseTimeScale);

        hero.playerDataModel.controllable = true;
    }
}
