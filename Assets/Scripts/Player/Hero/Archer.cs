using System.Collections;
using Unity.VisualScripting;
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
            StartCoroutine(JumpCharger());
        }

        return isPressed;
    }

    protected override void ChargeJump()
    {
        playerDataModel.playerMovement.dirModifier += Vector3.up * playerDataModel.jumpPower * 1.5f * jumpCharge * 0.01f;
        playerDataModel.jumpCount++;
        animator.SetTrigger("JumpH");
        animator.SetTrigger("JumpL");
    }
}
