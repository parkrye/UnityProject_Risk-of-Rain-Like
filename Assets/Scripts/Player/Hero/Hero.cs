using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    public PlayerDataModel playerDataModel;
    public Animator animator;
    public Skill[] skills = new Skill[4];

    protected virtual void Awake() 
    {
        animator = GetComponentInChildren<Animator>();
    }

    public abstract bool Jump(bool isPressed);

    public abstract bool Action1(bool isPressed);

    public abstract bool Action2(bool isPressed);

    public abstract bool Action3(bool isPressed);

    public abstract bool Action4(bool isPressed);

}
