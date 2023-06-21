using UnityEngine;

public class SkillDescUI : PopUpUI
{
    public void Setting(Skill skill)
    {
        texts["SkillDescText"].text
            = $"[{skill.SkillName}]\n" +
            $"{skill.SkillDesc}";
    }
}
