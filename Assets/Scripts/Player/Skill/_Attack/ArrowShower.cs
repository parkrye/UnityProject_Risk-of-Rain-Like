using UnityEngine;

public class ArrowShower : MonoBehaviour
{
    [SerializeField] Arrow[] arrows;

    void Awake()
    {
        arrows = GetComponentsInChildren<Arrow>();
    }

    public void Shot(Transform target, float damage)
    {
        foreach (var arrow in arrows)
        {
            arrow.Shot(target.position, damage);
        }
    }
}
