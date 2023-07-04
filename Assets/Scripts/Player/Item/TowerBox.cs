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

    protected override void OnEnable()
    {
        base.OnEnable();
        slider = GetComponentInChildren<Slider>();

        boxObject.SetActive(true);
        towerObject.SetActive(false);
        box = true;
        slider.maxValue = cost;
        slider.minValue = 0;
        slider.value = cost;
    }

    protected override IEnumerator LookRoutine()
    {
        while (isActiveAndEnabled)
        {
            if (box)
                costObject.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
            else
                hpObject.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
            yield return new WaitForEndOfFrame();
        }
    }

    public override void Interact()
    {
        if (box && GameManager.Data.Player.Coin >= cost)
        {
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
            GameObject followBolt = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/FollowEnergyBolt"), attackTransform.position, Quaternion.identity, true);
            followBolt.GetComponent<FollowBolt>().Shot(transform.forward, cost * 0.1f, 0f);
            slider.value -= cost * 0.1f;
            yield return new WaitForSeconds(1f);
        }
        GameManager.Resource.Destroy(gameObject);
    }
}
