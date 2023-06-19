using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Skill : ScriptableObject
{
    public Hero hero;
    public string SkillName;
    public string SkillDesc;
    public Sprite SkillIcon;

    public float coolTime, modifier;
    bool coolCheck;

    [SerializeField] protected string[] actionKeys = {"Action1", "Action2", "Action3", "Action4"};
    
    public bool CoolCheck
    {
        get { return coolCheck; }
        set
        {
            coolCheck = value;
            CoolEvent?.Invoke(CoolCheck);
        }
    }
    public UnityEvent<bool> CoolEvent;

    private void OnEnable()
    {
        CoolCheck = true;
    }

    public abstract bool Active(bool isPressed);

    public IEnumerator CoolTime(float modifier)
    {
        while (CoolCheck)
        {
            yield return null;
        }
        yield return new WaitForSeconds(coolTime * modifier);
        CoolCheck = true;
    }

}
