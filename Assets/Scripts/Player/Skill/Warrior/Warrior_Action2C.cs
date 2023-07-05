using System.Collections;
using UnityEngine;

/// <summary>
/// ¹ßÂ÷±â
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action2C", menuName = "Data/Skill/Warrior/Action2C")]
public class Warrior_Action2C : Skill, IEnumeratable, ICriticable
{
    public float knockbackDistance;
    float damage;
    [SerializeField] ParticleSystem dirt;

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

    public IEnumerator enumerator()
    {
        yield return new WaitForSeconds(0.08f * hero.playerDataModel.ReverseTimeScale);

        GameManager.Resource.Instantiate(dirt, hero.playerDataModel.playerAction.AttackTransform.position, Quaternion.identity, true);
        Collider[] colliders = Physics.OverlapSphere(hero.playerDataModel.playerAction.closeAttackTransform.position, hero.playerDataModel.playerAction.closeAttackRange);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (!colliders[i].CompareTag("Player"))
            {
                IHitable hittable = colliders[i].GetComponent<IHitable>();
                hittable?.Hit(damage * modifier, 0f);

                IMezable mazable = colliders[i].GetComponent<IMezable>();
                mazable?.KnockBack(knockbackDistance, hero.playerDataModel.playerTransform);
            }
        }
    }
}