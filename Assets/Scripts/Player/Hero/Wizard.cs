public class Wizard : Hero
{
    override protected void Awake()
    {
        base.Awake();
        coolTimes[0] = 1f;
        coolTimes[1] = 1f;
        coolTimes[2] = 4f;
        coolTimes[3] = 20f;
    }

    public override bool Jump(bool isPressed)
    {
        if (isPressed)
        {
            playerDataModel.jumpCount++;
            playerDataModel.isJump = true;
            playerDataModel.animator.SetTrigger("JumpH");
            playerDataModel.animator.SetTrigger("JumpL");
        }

        return isPressed;
    }

    public override bool Action1(bool isPressed, float coolTime)
    {
        if (coolChecks[0] && isPressed)
        {
            animator.SetTrigger("Action1");

            coolChecks[0] = false;
            StartCoroutine(CoolTime(0, coolTime));
            return true;
        }
        return false;
    }

    public override bool Action2(bool isPressed, float coolTime)
    {
        if (coolChecks[1] && isPressed)
        {
            animator.SetTrigger("Action2");

            coolChecks[1] = false;
            StartCoroutine(CoolTime(1, coolTime));
            return true;
        }
        return false;
    }

    public override bool Action3(bool isPressed, float coolTime)
    {
        if (coolChecks[2] && isPressed)
        {
            animator.SetTrigger("Action3");

            coolChecks[2] = false;
            StartCoroutine(CoolTime(2, coolTime));
            return true;
        }
        return false;
    }

    public override bool Action4(bool isPressed, float coolTime)
    {
        if (coolChecks[3] && isPressed)
        {
            animator.SetTrigger("Action4");

            coolChecks[3] = false;
            StartCoroutine(CoolTime(3, coolTime));
            return true;
        }
        return false;
    }
}
