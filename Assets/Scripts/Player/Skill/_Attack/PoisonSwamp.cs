using UnityEngine;

public class PoisonSwamp : MonoBehaviour
{
    float damage;

    public void SpwanSwamp(float _damage, float range, float time)
    {
        damage = _damage;
        transform.localScale = new Vector3(range, 1f, range);
        GameManager.Resource.Destroy(gameObject, time);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log(other.name);
            other.GetComponent<IHitable>()?.Hit(damage, 0f);
        }
    }
}
