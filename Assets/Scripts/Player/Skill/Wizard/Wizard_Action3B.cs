using UnityEngine;

/// <summary>
/// ½Çµå
/// </summary>
[CreateAssetMenu(fileName = "Wizard_Action3B", menuName = "Data/Skill/Wizard/Action3B")]
public class Wizard_Action3B : Skill, IDamageSubscriber
{
    public float shieldPoint;
    bool summonMagicFlat;
    GameObject magicFlat;

    public override bool Active(bool isPressed, params float[] param)
    {
        if (isPressed)
        {
            hero.playerDataModel.animator.SetTrigger(actionKeys[actionNum]);
            if(!summonMagicFlat)
                magicFlat = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Particle/MagicFlat"), hero.transform.position, Quaternion.identity, hero.transform, true);
            summonMagicFlat = true;
            shieldPoint = param[0];
            hero.playerDataModel.AddDamageSubscriber(this);
            CoolCheck = false;
            return true;
        }
        return false;
    }

    public float ModifiyDamage(float _damage)
    {
        if(_damage > shieldPoint)
        {
            hero.playerDataModel.RemoveDamageSubscriber(this);
            GameManager.Resource.Destroy(magicFlat);
            summonMagicFlat = false;
            return _damage - shieldPoint;
        }
        else
        {
            shieldPoint -= _damage;
            return 0f;
        }
    }
}
