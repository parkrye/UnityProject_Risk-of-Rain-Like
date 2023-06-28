using TMPro;
using UnityEngine;

public class MerchantBox : MonoBehaviour
{
    ItemData item;
    int cost;
    bool fall;
    [SerializeField] GameObject costObject;

    void Awake()
    {
        ItemData[] items = GameManager.Resource.LoadAll<ItemData>("Item");
        cost = (int)((Random.Range(1, 10) + GameManager.Data.Records["Time"] * 0.016f) * GameManager.Data.Records["Difficulty"]);
        item = items[Random.Range(0, items.Length)];
        fall = true;
        costObject.GetComponentInChildren<TextMeshProUGUI>().text = cost.ToString();
    }

    void Update()
    {
        if (fall)
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(ray, 2f, LayerMask.GetMask("Ground")))
                fall = false;
        }
    }

    void FixedUpdate()
    {
        if (fall)
            transform.Translate(Vector3.down * Time.deltaTime * 10f);
        transform.Rotate(Vector3.up);
    }

    void LateUpdate()
    {
        costObject.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }

    public void Interact()
    {
        if (GameManager.Data.Player.Coin >= cost)
        {
            GameManager.Data.Player.Coin -= cost;
            GameManager.Data.Player.inventory.AddItem(item);
            GameManager.Resource.Destroy(gameObject);
        }
    }
}
