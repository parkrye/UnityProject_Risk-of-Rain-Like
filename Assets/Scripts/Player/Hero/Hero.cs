using System.Collections;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    public PlayerDataModel playerDataModel;
    public Animator animator;
    public Skill[] skills;
    public Transform attackTransform;

    [SerializeField] protected int jumpCharge;
    protected bool nowCharge;

    protected virtual void Awake() 
    {
        animator = GetComponentInChildren<Animator>();
        Skill[] skills = new Skill[4];
    }

    public void SettingSkill(int skillNum, Skill skill)
    {
        if (skillNum < 0 || skillNum >= skills.Length || skill == null)
        {
            return;
        }

        skills[skillNum] = skill;
        skill.hero = this;
    }

    public abstract bool Jump(bool isPressed);

    public abstract bool Action1(bool isPressed);

    public abstract bool Action2(bool isPressed);

    public abstract bool Action3(bool isPressed);

    public abstract bool Action4(bool isPressed);

    protected IEnumerator JumpCharger()
    {
        jumpCharge = 80;
        nowCharge = true;
        while (jumpCharge < 120 && nowCharge)
        {
            jumpCharge++;
            yield return new WaitForSeconds(0.001f);
        }
        ChargeJump();
    }

    protected abstract void ChargeJump();
}
