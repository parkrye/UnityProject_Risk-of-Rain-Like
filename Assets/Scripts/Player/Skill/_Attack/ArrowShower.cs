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
        for(int i = 0; i < arrows.Length; i++)
        {
            arrows[i].Shot(target.position, damage);
        }
    }
}
