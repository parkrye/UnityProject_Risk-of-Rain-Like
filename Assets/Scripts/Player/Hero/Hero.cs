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
    protected string heroName;

    public UnityEvent<bool[]> ActionEvent;

    protected virtual void Awake() 
    {
        animator = GetComponentInChildren<Animator>();
        Skill[] skills = new Skill[4];
    }

    public void SettingSkill(int slot, Skill skill)
    {
        if (slot < 0 || slot >= skills.Length || skill == null)
        {
            return;
        }

        skills[slot] = skill;
        skill.hero = this;
        skill.CoolEvent.AddListener(CallCoolTime);
    }

    public void SettingSkill(int slot, int skillNum)
    {
        switch (skillNum)
        {
            case 1:
                SettingSkill(slot - 1, GameManager.Resource.Load<Skill>($"Skill/{heroName}/{heroName}_Action{slot}A"));
                break;
            case 2:
                SettingSkill(slot - 1, GameManager.Resource.Load<Skill>($"Skill/{heroName}/{heroName}_Action{slot}B"));
                break;
            case 3:
                SettingSkill(slot - 1, GameManager.Resource.Load<Skill>($"Skill/{heroName}/{heroName}_Action{slot}C"));
                break;
        }
    }
    
    void CallCoolTime(bool cool)
    {
        ActionEvent?.Invoke(new bool[] { skills[0].CoolCheck, skills[1].CoolCheck, skills[2].CoolCheck, skills[3].CoolCheck });
    }

    public abstract bool Jump(bool isPressed);

    public bool Action(int num, bool isPressed)
    {
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
