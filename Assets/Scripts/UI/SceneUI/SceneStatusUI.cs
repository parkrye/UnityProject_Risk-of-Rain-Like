public class SceneStatusUI : BaseUI
{
    void Start()
    {
        UpdateLevel();
        UpdateHP();
        UpdateEXP();

        GameManager.Data.Player.AddEventListener(GameManager.Data.Player.LevelEvent, UpdateLevel);
        GameManager.Data.Player.AddEventListener(GameManager.Data.Player.HPEvent, UpdateHP);
        GameManager.Data.Player.AddEventListener(GameManager.Data.Player.EXPEvent, UpdateEXP);
    }

    public void UpdateLevel()
    {
        texts["LevelText"].text = GameManager.Data.Player.level.ToString();
        sliders["HPBar"].maxValue = GameManager.Data.Player.maxHP;
        sliders["EXPBar"].maxValue = GameManager.Data.Player.level * 100f;
    }

    public void UpdateHP()
    {
        sliders["HPBar"].value = GameManager.Data.Player.nowHP;
    }

    public void UpdateEXP()
    {
        sliders["EXPBar"].value = GameManager.Data.Player.exp;
    }
}
