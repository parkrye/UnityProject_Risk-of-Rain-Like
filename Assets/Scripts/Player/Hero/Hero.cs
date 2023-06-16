using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Hero : MonoBehaviour
{
    public PlayerDataModel playerDataModel;
    public Animator animator;
    public Skill[] skills;
    public Transform attackTransform;

    [SerializeField] protected int jumpCharge;
    protected bool nowCharge;

    public UnityEvent<bool[]> ActionEvent;

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
        skill.CoolEvent.AddListener(CallCoolTime);
    }

    void CallCoolTime(bool cool)
    {
        ActionEvent?.Invoke(new bool[] { skills[0].CoolCheck, skills[1].CoolCheck, skills[2].CoolCheck, skills[3].CoolCheck });
    }

    public abstract bool Jump(bool isPressed);

    public bool Action(int num, bool isPressed)
    {
        for(int i = 0; i < 4; i++)
        {
            if(i != num)
            {
                skills[i].Active(false);
            }
        }

        if (skills[num].CoolCheck && skills[num].Active(isPressed))
        {
            if (skills[num] is IEnumeratable)
                StartCoroutine((skills[num] as IEnumeratable).enumerator());
            StartCoroutine(skills[num].CoolTime(playerDataModel.coolTime));
            return true;
        }
        return false;
    }

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
