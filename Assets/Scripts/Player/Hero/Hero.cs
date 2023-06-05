using System.Collections;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    public float[] coolTimes = new float[4];
    public bool[] coolChecks = new bool[4];
    Animator animator;
    public Animator Animator { get { return animator; } }

    void Awake()
    {
        for(int i = 0; i < coolChecks.Length; i++)
            coolChecks[i] = true;
        animator = GetComponentInChildren<Animator>();
    }

    public abstract bool Jump(bool isPressed);

    public abstract bool Action1(bool isPressed, float coolTime);

    public abstract bool Action2(bool isPressed, float coolTime);

    public abstract bool Action3(bool isPressed, float coolTime);

    public abstract bool Action4(bool isPressed, float coolTime);

    protected IEnumerator CoolTime(int actionNum, float coolTime)
    {
        yield return new WaitForSeconds(coolTimes[actionNum] * coolTime);
        coolChecks[actionNum] = true;
    }
}
