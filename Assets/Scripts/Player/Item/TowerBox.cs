using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TowerBox : MerchantBox
{
    bool box;
    Slider slider;
    [SerializeField] GameObject hpObject;
    [SerializeField] GameObject boxObject, towerObject;
    [SerializeField] Transform attackTransform;
    [SerializeField] MeshRenderer towerRenderer;
    [SerializeField] Material attackModeMaterial, healModeMaterial;
    enum SupportType { Attack, Heal }
    [SerializeField] SupportType supportType;
    FollowBolt followBolt;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        followBolt = GameManager.Resource.Load<FollowBolt>("Attack/FollowEnergyBolt");
        towerRenderer = towerObject.GetComponent<MeshRenderer>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        supportType = SupportType.Attack;
        towerRenderer.material = attackModeMaterial;
        if (Random.Range(0, 2) == 1)
        {
            supportType = SupportType.Heal;
            towerRenderer.material = healModeMaterial;
        }

        boxObject.SetActive(true);
        towerObject.SetActive(false);
        box = true;
        slider.maxValue = cost;
        slider.minValue = 0;
        slider.value = cost;
    }

    public override void Interact()
    {
        if (box && GameManager.Data.Player.Coin >= cost)
        {
            StopAllCoroutines();
            GameManager.Data.Player.Coin -= cost;
            boxObject.SetActive(false);
            towerObject.SetActive(true);
            box = false;

            StartCoroutine(TowerRoutine());
        }
    }

    IEnumerator TowerRoutine()
    {
        while(slider.value > 0)
        {
            switch (supportType)
            {
                case SupportType.Attack:
                    FollowBolt boltAttack = GameManager.Resource.Instantiate(followBolt, attackTransform.position, Quaternion.identity, true);
                    boltAttack.Shot(transform.forward, cost * 0.1f, 0f);
                    slider.value -= cost * 0.1f;
                    break;
                case SupportType.Heal:
                    GameManager.Data.Player.NOWHP += cost * 0.5f;
                    slider.value -= cost * 0.1f;
                    break;
            }
            yield return new WaitForSeconds(1f);
        }
        GameManager.Resource.Destroy(gameObject);
    }

    protected override IEnumerator LookRoutine()
    {
        while (this)
        {
            if (box)
                costObject.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
            else
                hpObject.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
            yield return new WaitForEndOfFrame();
        }
    }
}
