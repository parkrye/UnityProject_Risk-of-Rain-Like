using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Archer : Hero
{
    override protected void Awake()
    {
        base.Awake();
        SettingSkill(0, GameManager.Resource.Load<Skill>("Skill/Archer/Archer_Action1A"));
        SettingSkill(1, GameManager.Resource.Load<Skill>("Skill/Archer/Archer_Action2A"));
        SettingSkill(2, GameManager.Resource.Load<Skill>("Skill/Archer/Archer_Action3A"));
        SettingSkill(3, GameManager.Resource.Load<Skill>("Skill/Archer/Archer_Action4A"));
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
        playerDataModel.rb.velocity = new Vector3(playerDataModel.rb.velocity.x, playerDataModel.jumpPower * 1.5f * jumpCharge * 0.01f, playerDataModel.rb.velocity.z);
        playerDataModel.jumpCount++;
        animator.SetTrigger("JumpH");
        animator.SetTrigger("JumpL");
    }
}
