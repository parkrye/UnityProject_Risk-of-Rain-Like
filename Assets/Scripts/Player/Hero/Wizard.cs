using System.Collections;
using UnityEngine;

public class Wizard : Hero
{
    bool floating = false;

    override protected void Awake()
    {
        base.Awake();

        heroName = "Wizard";
    }

    public override bool Jump(bool isPressed)
    {
        if (nowCharge)
            nowCharge = false;
        if(floating)
            floating = false;

        if (isPressed)
        {
            playerDataModel.jumpCount++;
            floating = true;
            playerDataModel.playerMovement.dirModifier += Vector3.up * playerDataModel.JumpPower * 1f;
            StartCoroutine(FloaterJump());
            animator.SetTrigger("JumpH");
            animator.SetTrigger("JumpL");
        }

        return isPressed;
    }

    protected override void ChargeJump() { }

    IEnumerator FloaterJump()
    {
        yield return new WaitForSeconds(0.2f / playerDataModel.TimeScale);
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
