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
            StartCoroutine(JumpCharger());
        }

        return isPressed;
    }

    protected override void ChargeJump()
    {
        playerDataModel.playerMovement.dirModifier += Vector3.up * playerDataModel.jumpPower * 1.2f * jumpCharge * 0.01f;
        playerDataModel.jumpCount++;
        animator.SetTrigger("JumpH");
        animator.SetTrigger("JumpL");
    }
}
