using System.Collections;
using UnityEngine;

/// <summary>
/// 주변베기
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action1C", menuName = "Data/Skill/Warrior/Action1C")]
public class Warrior_Action1C : Skill, IEnumeratable, ICriticable
{
    public float skillRangeModifier;
    float damage;
    [SerializeField] ParticleSystem slash;

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

        GameManager.Resource.Instantiate(slash, hero.playerDataModel.playerAction.AttackTransform.position, hero.playerDataModel.playerTransform.rotation, true);

        Collider[] colliders = Physics.OverlapSphere(hero.playerDataModel.playerTransform.position + Vector3.up, hero.playerDataModel.playerAction.closeAttackRange * skillRangeModifier);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (!colliders[i].CompareTag("Player"))
            {
                IHitable hittable = colliders[i].GetComponent<IHitable>();
                hittable?.Hit(damage * modifier, 0f);
            }
        }
    }
}