using UnityEngine;

public class AchivementElementUI : SceneUI
{
    public override void Initialize()
    {

    }

    public void SetContent(Sprite icon, string title, string content)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        images["AchiveIcon"].sprite = icon;
        texts["AchiveTitle"].text = title;
        texts["AchiveContent"].text = content;
    }
}
