using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu (fileName = "Skill", menuName = "Data/Skill")]
public abstract class Skill : ScriptableObject
{
    public string SkillName;
    public string SkillDesc;
    public Sprite SkillIcon;

    public float coolTime;
    public bool coolCheck;

    void Awake()
    {
        coolCheck = true;
    }

    public abstract bool Active(bool isPressed);

    public IEnumerator CoolTime(float modifier)
    {
        yield return new WaitForSeconds(coolTime * modifier);
        coolCheck = true;
    }
}
