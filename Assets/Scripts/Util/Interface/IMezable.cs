using System.Collections;
using UnityEngine;

public interface IMezable
{
    public void Stuned(float time);

    public void Slowed(float time, float modifier);

    public void KnockBack(float distance, Transform backFrom);

    IEnumerator StunRoutine(float time);

    IEnumerator SlowRoutine(float time, float modifier);

    IEnumerator KnockBackRoutine(float distance, Transform backFrom);
}
