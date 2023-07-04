using System.Collections;
using UnityEngine;

/// <summary>
/// ½´ÆÛ Á¡ÇÁ
/// </summary>
[CreateAssetMenu(fileName = "Wizard_Action3C", menuName = "Data/Skill/Wizard/Action3C")]
public class Wizard_Action3C : Skill, IEnumeratable
{
    public float jumpPower, skillRange, dodgeTime;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);

            ParticleSystem effect = GameManager.Resource.Instantiate(GameManager.Resource.Load<ParticleSystem>("Particle/FireworkBlueLarge"), hero.playerDataModel.playerTransform.position, Quaternion.identity, hero.playerDataModel.playerTransform, true);
            Collider[] colliders = Physics.OverlapSphere(hero.playerDataModel.playerTransform.position, skillRange);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (!colliders[i].CompareTag("Player"))
                {
                    IHitable hittable = colliders[i].GetComponent<IHitable>();
                    hittable?.Hit(param[0] * modifier, 0f);
                }
            }

            hero.playerDataModel.playerMovement.dirModifier += Vector3.up * jumpPower;

            CoolCheck = false;
            return true;
        }
        return false;
    }

    public IEnumerator enumerator()
    {
        hero.playerDataModel.dodgeDamage = true;
        yield return new WaitForSeconds(dodgeTime);
        hero.playerDataModel.dodgeDamage = false;
    }
}
