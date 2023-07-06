using System.Text;

public class DescUI : PopUpUI
{
    public void Setting(Skill skill)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(skill.SkillName);
        stringBuilder.Append("\n");
        stringBuilder.Append(skill.SkillDesc);

        Setting(stringBuilder.ToString());
    }
    public void Setting(ItemData item)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(item.ItemName);
        stringBuilder.Append("\n");
        stringBuilder.Append(item.ItemDesc);

        Setting(stringBuilder.ToString());
    }

    public void Setting(string text)
    {
        texts["DescText"].text = text;
    }
}
