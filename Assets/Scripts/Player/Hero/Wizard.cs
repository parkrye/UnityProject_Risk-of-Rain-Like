public class Wizard : Hero
{
    override protected void Awake()
    {
        base.Awake();

    }

    public override bool Jump(bool isPressed)
    {
        if (isPressed)
        {
            playerDataModel.jumpCount++;
            playerDataModel.isJump = true;
            animator.SetTrigger("JumpH");
            animator.SetTrigger("JumpL");
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
}
