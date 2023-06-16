using System.Collections;
using UnityEngine;

public class Wizard : Hero
{
    bool floating = false;

    override protected void Awake()
    {
        base.Awake();

        SettingSkill(0, GameManager.Resource.Load<Skill>("Skill/Wizard/Wizard_Action1A"));
        SettingSkill(1, GameManager.Resource.Load<Skill>("Skill/Wizard/Wizard_Action2A"));
        SettingSkill(2, GameManager.Resource.Load<Skill>("Skill/Wizard/Wizard_Action3A"));
        SettingSkill(3, GameManager.Resource.Load<Skill>("Skill/Wizard/Wizard_Action4A"));
    }

    public override bool Jump(bool isPressed)
    {
        if (nowCharge)
            nowCharge = false;
        if(floating)
            floating = false;

        if (isPressed)
        {
            floating = true;
            playerDataModel.playerMovement.dirModifier += Vector3.up * playerDataModel.jumpPower * 1f;
            StartCoroutine(FloaterJump());
            animator.SetTrigger("JumpH");
            animator.SetTrigger("JumpL");
        }

        return isPressed;
    }

    protected override void ChargeJump() { }

    IEnumerator FloaterJump()
    {
        yield return new WaitForSeconds(0.2f);
        playerDataModel.jumpCount++;
        playerDataModel.playerMovement.dirModifier.y = 0f;
        playerDataModel.rb.useGravity = false;
        while (floating)
        {
            yield return new WaitForFixedUpdate();
            if (Mathf.Abs(playerDataModel.rb.velocity.y) > 1f)
                floating = false;
        }
        playerDataModel.rb.useGravity = true;
    }
}
