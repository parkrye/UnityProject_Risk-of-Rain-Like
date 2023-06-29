using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionController : MonoBehaviour
{
    PlayerDataModel playerDataModel;

    public Transform AttackTransform;
    public Transform lookAtTransform, lookFromTransform, interactTransform, closeAttackTransform;
    public float closeAttackRange, interactRange;
    float cosResult;
    ESCUI escUI;

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
        Vector3 playerPosition = transform.position;
        Vector3 playerLook = new Vector3(transform.forward.x, 0f, transform.forward.z);
        playerPosition.y = 0f;
        foreach (Collider collider in colliders)
        {
            IInteractable interactable = collider.GetComponent<IInteractable>();
            if (interactable is null)
                continue;
            Vector3 colliderPosition = collider.transform.position;
            colliderPosition.y = 0f;
            Vector3 dirTarget = (colliderPosition - playerPosition).normalized;
            if (Vector3.Dot(playerLook, dirTarget) < cosResult)
                continue;

            interactable?.Interact();
        }
    }

    void OnInteract(InputValue inputValue)
    {
        Interact();
    }

    void OnESC(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            if (GameManager.Scene.CurScene.name.StartsWith("LevelScene") && GameManager.Scene.ReadyToPlay)
            {
                if (!playerDataModel.onESC)
                {
                    escUI = GameManager.UI.ShowPopupUI<ESCUI>("UI/ESCUI");
                }
                else
                {
                    escUI?.CloseUI();
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (playerDataModel.onGizmo)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(closeAttackTransform.position, closeAttackRange);
        }
    }
}
