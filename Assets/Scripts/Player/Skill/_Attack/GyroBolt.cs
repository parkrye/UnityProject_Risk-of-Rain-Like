using UnityEngine;

public class GyroBolt : BoltType
{
    float drawPower;
    [SerializeField] Transform size;

    public void Shot(Vector3 target, float _damage, float _delay, float _drawPower, float _drawRange)
    {
        (coll as SphereCollider).radius = _drawRange;
        size.localScale = Vector3.one * _drawRange * 2f;
        drawPower = _drawPower;
        Shot(target, _damage, _delay);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IHitable>()?.Hit(damage, 0f);
            other.GetComponent<ITranslatable>()?.TranslateGradually((transform.position - other.transform.position).normalized, drawPower * Time.deltaTime);
            //other.transform.Translate(drawPower * Time.deltaTime * (transform.position - other.transform.position).normalized);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {

    }
}
