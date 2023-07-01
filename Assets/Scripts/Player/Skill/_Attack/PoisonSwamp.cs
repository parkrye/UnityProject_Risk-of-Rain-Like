using System.Collections;
using UnityEngine;

public class PoisonSwamp : MonoBehaviour
{
    float damage;

    public void SpwanSwamp(float _damage, float range, float time)
    {
        damage = _damage;
        transform.localScale = new Vector3(range, 1f, range);
        transform.Translate(Vector3.down * 10f);
        StartCoroutine(FillUp(time));

        for(int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                ParticleSystem effect = GameManager.Resource.Instantiate(GameManager.Resource.Load<ParticleSystem>("Particle/_poison"), transform, true);
                effect.transform.position = transform.position + Vector3.forward * i * 0.2f * range + Vector3.right * j * 0.2f * range + Vector3.up;
            }
        }
    }

    IEnumerator FillUp(float time)
    {
        for (int i = 0; i < 56; i++)
        {
            transform.Translate(Vector3.up * 0.184f);
            yield return new WaitForFixedUpdate();
        }
        GameManager.Resource.Destroy(gameObject, time);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IHitable>()?.Hit(damage, 0f);
        }
    }
}
