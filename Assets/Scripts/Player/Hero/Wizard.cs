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
            playerDataModel.rb.velocity = new Vector3(playerDataModel.rb.velocity.x, playerDataModel.jumpPower * 1f, playerDataModel.rb.velocity.z);
            StartCoroutine(FloaterJump());
            animator.SetTrigger("JumpH");
            animator.SetTrigger("JumpL");
        }

        return isPressed;
    }

    protected override void ChargeJump() { }

    IEnumerator FloaterJump()
    {
        yield return new WaitForSeconds(0.5f);
        playerDataModel.rb.velocity = new Vector3(playerDataModel.rb.velocity.x, 0f, playerDataModel.rb.velocity.z);
        playerDataModel.jumpCount++;
        playerDataModel.rb.useGravity = false;
        while (floating)
        {
            yield return new WaitForFixedUpdate();
            if (Mathf.Abs(playerDataModel.rb.velocity.y) > 0.1f)
                floating = false;
        }
        playerDataModel.rb.useGravity = true;
    }
}
