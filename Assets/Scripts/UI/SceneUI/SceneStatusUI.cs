public class SceneStatusUI : SceneUI
{
    public override void Initialize()
    {
        UpdateLevel();
        UpdateHP();
        UpdateEXP();

        GameManager.Data.Player.LevelEvent.AddListener(UpdateLevel);
        GameManager.Data.Player.HPEvent.AddListener(UpdateHP);
        GameManager.Data.Player.EXPEvent.AddListener(UpdateEXP);
    }

    public void UpdateLevel()
    {
        texts["LevelText"].text = $"Lv {GameManager.Data.Player.LEVEL}";
        sliders["HPBar"].maxValue = GameManager.Data.Player.MAXHP;
        sliders["EXPBar"].maxValue = GameManager.Data.Player.LEVEL * 100f;
    }

    public void UpdateHP()
    {
        sliders["HPBar"].maxValue = GameManager.Data.Player.MAXHP;
        sliders["HPBar"].value = GameManager.Data.Player.NOWHP;
    }

    public void UpdateEXP()
    {
        sliders["EXPBar"].value = GameManager.Data.Player.EXP;
    }
}
