using System;
using System.Collections;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public Hero hero;
    public string SkillName;
    public string SkillDesc;
    public Sprite SkillIcon;

    public float coolTime, modifier;
    public bool coolCheck;

    private void OnEnable()
    {
        coolCheck = true;
    }

    public abstract bool Active(bool isPressed);

    public IEnumerator CoolTime(float modifier)
    {
        while (coolCheck)
        {
            yield return null;
        }
        yield return new WaitForSeconds(coolTime * modifier);
        coolCheck = true;
    }
}
