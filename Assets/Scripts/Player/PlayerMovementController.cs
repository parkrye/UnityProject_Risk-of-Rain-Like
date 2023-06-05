using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    PlayerDataModel playerDataModel;

    [SerializeField] Vector3 moveDir;

    void Awake()
    {
        playerDataModel = GetComponent<PlayerDataModel>();
    }

    void Update()
    {
        CheckGround();
    }

    void CheckGround()
    {
        Debug.DrawRay(transform.position + transform.up * 0.1f, -transform.up * 0.2f, Color.red);
        if (Physics.Raycast(transform.position + transform.up * 0.1f, -transform.up, 0.2f, LayerMask.GetMask("Ground")))
        {
            playerDataModel.jumpCount = 0;
            playerDataModel.animator.SetBool("IsGround", true);
            return;
        }
        playerDataModel.animator.SetBool("IsGround", false);
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {
        playerDataModel.rb.AddForce(moveDir * playerDataModel.moveSpeed, ForceMode.Force);
    }

    void Jump()
    {
        if (playerDataModel.isJump)
        {
            playerDataModel.rb.AddForce(transform.up * playerDataModel.jumpPower);
            playerDataModel.isJump = false;
        }
    }

    void OnMove(InputValue inputValue)
    {
        Vector2 tmp = inputValue.Get<Vector2>();
        moveDir = new Vector3(tmp.x, 0f, tmp.y);
        if (moveDir ==  Vector3.zero)
        {
            playerDataModel.animator.SetFloat("Move", 0);
        }
        else
        {
            playerDataModel.animator.SetFloat("Move", 1);
        }
    }

    void OnJump(InputValue inputValue)
    {
        if(playerDataModel.jumpCount < playerDataModel.jumpLimit && playerDataModel.hero.Jump(inputValue.isPressed))
        {
            playerDataModel.jumpCount++;
            playerDataModel.isJump = true;
            playerDataModel.animator.SetTrigger("Jump");
        }
    }
}
