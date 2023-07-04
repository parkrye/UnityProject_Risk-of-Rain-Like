using UnityEngine;

public class Flame : MonoBehaviour
{
    float damage;

    public void StartFlame(float _damage)
    {
        damage = _damage;
    }

    public void StopFlame()
    {
        GameManager.Resource.Destroy(gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IHitable>()?.Hit(damage, 0f);
        }
    }
}
