using UnityEngine;

public class Warrior : Hero
{
    override protected void Awake()
    {
        base.Awake();
        SettingSkill(0, GameManager.Resource.Load<Skill>("Skill/Warrior/Warrior_Action1A"));
        SettingSkill(1, GameManager.Resource.Load<Skill>("Skill/Warrior/Warrior_Action2A"));
        SettingSkill(2, GameManager.Resource.Load<Skill>("Skill/Warrior/Warrior_Action3A"));
        SettingSkill(3, GameManager.Resource.Load<Skill>("Skill/Warrior/Warrior_Action4A"));
    }

    public override bool Jump(bool isPressed)
    {
        if (nowCharge)
            nowCharge = false;

        if (isPressed)
        {
            if (playerDataModel.jumpCount < playerDataModel.jumpLimit)
            {
                StartCoroutine(JumpCharger());
            }
        }

        return isPressed;
    }

    public override bool Action1(bool isPressed)
    {
        if (skills[0].Active(isPressed))
        {
            StartCoroutine(skills[0].CoolTime(GameManager.Data.Player.coolTime));
            return true;
        }
        return false;
    }

    public override bool Action2(bool isPressed)
    {
        if (skills[1].Active(isPressed))
        {
            StartCoroutine(skills[1].CoolTime(GameManager.Data.Player.coolTime));
            return true;
        }
        return false;
    }

    public override bool Action3(bool isPressed)
    {
        if (skills[2].Active(isPressed))
        {
            StartCoroutine(skills[2].CoolTime(GameManager.Data.Player.coolTime));
            return true;
        }
        return false;
    }

    public override bool Action4(bool isPressed)
    {
        if (skills[3].Active(isPressed))
        {
            StartCoroutine(skills[3].CoolTime(GameManager.Data.Player.coolTime));
            return true;
        }
        return false;
    }

    protected override void ChargeJump()
    {
        playerDataModel.rb.velocity = new Vector3(playerDataModel.rb.velocity.x, playerDataModel.jumpPower * 1f * jumpCharge * 0.01f, playerDataModel.rb.velocity.z);
        playerDataModel.jumpCount++;
        animator.SetTrigger("JumpH");
        animator.SetTrigger("JumpL");
    }
}
