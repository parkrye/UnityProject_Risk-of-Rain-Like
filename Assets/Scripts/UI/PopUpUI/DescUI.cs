public class DescUI : PopUpUI
{
    public void Setting(Skill skill)
    {
        texts["DescText"].text
            = $"[{skill.SkillName}]\n" +
            $"{skill.SkillDesc}";
    }
}
