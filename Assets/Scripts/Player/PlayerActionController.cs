using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionController : MonoBehaviour
{
    PlayerDataModel playerDataModel;
    public Transform AttackTransform;

    public Transform lookAtTransform, lookFromTransform, interactTransform;
    public float closeAttackRange, interactRange;
    float cosResult;

    void Awake()
    {
        playerDataModel = GetComponent<PlayerDataModel>();
        cosResult = Mathf.Cos(60f * Mathf.Deg2Rad);
    }

    void OnAction1(InputValue inputValue)
    {
        if(playerDataModel.hero.Action(0, inputValue.isPressed))
        {

        }
    }

    void OnAction2(InputValue inputValue)
    {
        if (playerDataModel.hero.Action(1, inputValue.isPressed))
        {

        }
    }

    void OnAction3(InputValue inputValue)
    {
        if (playerDataModel.hero.Action(2, inputValue.isPressed))
        {

        }
    }

    void OnAction4(InputValue inputValue)
    {
        if (playerDataModel.hero.Action(3, inputValue.isPressed))
        {

        }
    }

    public void Interact()
    {
        Collider[] colliders = Physics.OverlapSphere(interactTransform.position, interactRange);
        foreach (Collider collider in colliders)
        {
            IInteractable interactable = collider.GetComponent<IInteractable>();
            if (interactable == null)
                continue;
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, dirTarget) < cosResult)
                continue;

            interactable?.Interact();
        }
    }

    void OnInteract(InputValue inputValue)
    {
        Interact();
    }
}
