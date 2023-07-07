using System.Collections;
using UnityEngine;

/// <summary>
/// Æòº£±â
/// </summary>
[CreateAssetMenu(fileName = "Warrior_Action1A", menuName = "Data/Skill/Warrior/Action1A")]
public class Warrior_Action1A : Skill, IEnumeratable, ICriticable
{
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

    public IEnumerator Enumerator()
    {
        yield return new WaitForSeconds(0.08f * hero.playerDataModel.ReverseTimeScale);

        ParticleSystem effect = GameManager.Resource.Instantiate(slash, hero.playerDataModel.playerAction.AttackTransform.position, Quaternion.identity, true);
        effect.transform.LookAt(effect.transform.position + hero.playerDataModel.playerTransform.forward);

        Collider[] colliders = Physics.OverlapSphere(hero.playerDataModel.playerAction.closeAttackTransform.position, hero.playerDataModel.playerAction.closeAttackRange);
        for(int i = 0; i < colliders.Length; i++)
        {
            if (!colliders[i].CompareTag("Player"))
            {
                IHitable hittable = colliders[i].GetComponent<IHitable>();
                hittable?.Hit(damage * modifier, 0f);
            }
        }
    }
}
