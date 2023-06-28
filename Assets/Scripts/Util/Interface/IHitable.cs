using System.Collections;

public interface IHitable
{
    public void Hit(float damage, float time);

    IEnumerator HitRoutine(float damage, float time);

    public void Die();
}
