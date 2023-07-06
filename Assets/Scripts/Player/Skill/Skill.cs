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

    protected string[] actionKeys = {"Action1A", "Action2A", "Action3A", "Action4A",
                                    "Action1B", "Action2B", "Action3B", "Action4B",
                                    "Action1C", "Action2C", "Action3C", "Action4C"};
    [SerializeField] protected int actionNum;
    
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

    public abstract bool Active(bool isPressed, params float[] param);

    public IEnumerator CoolTime(float modifier)
    {
        while (CoolCheck)
            yield return null;
        yield return new WaitForSeconds(coolTime * modifier * hero.playerDataModel.ReverseTimeScale);
        CoolCheck = true;
    }
}
