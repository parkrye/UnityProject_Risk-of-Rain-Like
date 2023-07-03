public class SceneStatusUI : SceneUI
{
    public override void Initialize()
    {
        UpdateLevel();
        UpdateHP();
        UpdateEXP();

        GameManager.Data.Player.OnLevelEvent.RemoveAllListeners();
        GameManager.Data.Player.OnHPEvent.RemoveAllListeners();
        GameManager.Data.Player.OnEXPEvent.RemoveAllListeners();
        GameManager.Data.Player.OnLevelEvent.AddListener(UpdateLevel);
        GameManager.Data.Player.OnHPEvent.AddListener(UpdateHP);
        GameManager.Data.Player.OnEXPEvent.AddListener(UpdateEXP);
    }

    public void UpdateLevel()
    {
        texts["LevelText"].text = $"·¹º§ {GameManager.Data.Player.LEVEL}";
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
