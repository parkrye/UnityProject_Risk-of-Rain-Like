using System.Collections;
using Unity.EditorCoroutines.Editor;
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

            ParticleSystem effect = GameManager.Resource.Instantiate(GameManager.Resource.Load<ParticleSystem>("Particle/FireworkBlueLarge"), hero.playerDataModel.playerTransform.position, Quaternion.identity, true);
            GameManager.Resource.Destroy(effect.gameObject, 2f);
            Collider[] colliders = Physics.OverlapSphere(hero.playerDataModel.playerTransform.position, skillRange);
            foreach (Collider collider in colliders)
            {
                IHitable hittable = collider.GetComponent<IHitable>();
                hittable?.Hit(param[0] * modifier, 0f);
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
