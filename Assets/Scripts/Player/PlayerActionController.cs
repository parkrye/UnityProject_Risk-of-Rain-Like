using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionController : MonoBehaviour
{
    PlayerDataModel playerDataModel;
    public Transform AttackTransform;

    public Transform lookAtTransform;
    [SerializeField] Transform interactTransform;
    [SerializeField] float ransge;

    void Awake()
    {
        playerDataModel = GetComponent<PlayerDataModel>();
    }

    void OnAction1(InputValue inputValue)
    {
        if(playerDataModel.hero.Action1(inputValue.isPressed))
        {

        }
    }

    void OnAction2(InputValue inputValue)
    {
        if (playerDataModel.hero.Action2(inputValue.isPressed))
        {

        }
    }

    void OnAction3(InputValue inputValue)
    {
        if (playerDataModel.hero.Action3(inputValue.isPressed))
        {

        }
    }

    void OnAction4(InputValue inputValue)
    {
        if (playerDataModel.hero.Action4(inputValue.isPressed))
        {

        }
    }

    public void Interact()
    {
        Collider[] colliders = Physics.OverlapSphere(interactTransform.position, ransge);
        foreach (Collider collider in colliders)
        {
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, dirTarget) < Mathf.Cos(60f * 0.5f * Mathf.Deg2Rad))
                continue;

            IInteractable interactable = collider.GetComponent<IInteractable>();
            interactable?.Interact();
        }
    }

    void OnInteract(InputValue inputValue)
    {
        Interact();
    }
}
