using UnityEngine;

public class ESCUI : PopUpUI
{
    protected override void Awake()
    {
        base.Awake();
        buttons["ResumeButton"].onClick.AddListener(ResumeButton);
        buttons["OptionButton"].onClick.AddListener(OptionButton);
        buttons["BackTitleButton"].onClick.AddListener(BackTitleButton);
    }

    void OnEnable()
    {
        GameManager.Data.Player.onESC = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

    void ResumeButton()
    {
        GameManager.Data.Player.onESC = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        CloseUI();
    }

    void OptionButton()
    {
        GameManager.UI.ShowPopupUI<PopUpUI>("UI/OptionUI");
    }

    void BackTitleButton()
    {
        Time.timeScale = 1f;
        GameManager.Scene.LoadScene("TitleScene");
    }
}
