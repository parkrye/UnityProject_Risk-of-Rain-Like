using UnityEngine;

public class Archer : Hero
{
    override protected void Awake()
    {
        base.Awake();

        heroName = "Archer";
    }

    public override bool Jump(bool isPressed)
    {
        if (nowCharge)
            nowCharge = false;

        if (isPressed)
        {
            playerDataModel.jumpCount--;
            StartCoroutine(JumpCharger());
        }

        return isPressed;
    }

    protected override void ChargeJump()
    {
        playerDataModel.playerMovement.dirModifier += 0.01f * 2f * jumpCharge * playerDataModel.JumpPower * Vector3.up;
        animator.SetTrigger("JumpH");
        animator.SetTrigger("JumpL");
    }
}
