using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    PlayerDataModel playerDataModel;

    void Awake()
    {
        playerDataModel = GetComponent<PlayerDataModel>();
    }

    void Update()
    {
        CheckGround();
        CheckClimable();
    }

    void CheckGround()
    {
        if (!playerDataModel.isJump)
        {
            if (Physics.Raycast(transform.position + transform.up * 0.1f, -transform.up, 0.2f, LayerMask.GetMask("Ground")))
            {
                playerDataModel.jumpCount = 0;
                playerDataModel.animator.SetBool("IsGround", true);
                return;
            }
            playerDataModel.animator.SetBool("IsGround", false);
        }
    }

    void CheckClimable()
    {
        if (playerDataModel.moveDir == Vector3.zero)
            return;

        if(!playerDataModel.climable.ContainsKey(playerDataModel.moveDir))
            playerDataModel.climable.Add(playerDataModel.moveDir, false);

        Debug.DrawRay(transform.position + transform.up * playerDataModel.climbCheckLowHeight, (transform.forward * playerDataModel.moveDir.z + transform.right * playerDataModel.moveDir.x) * playerDataModel.climbCheckLength, Color.red, 2f);
        Debug.DrawRay(transform.position + transform.up * playerDataModel.climbCheckHighHeight, (transform.forward * playerDataModel.moveDir.z + transform.right * playerDataModel.moveDir.x) * playerDataModel.climbCheckLength, Color.red, 2f);
        if (Physics.Raycast(transform.position + transform.up * playerDataModel.climbCheckLowHeight, (transform.forward * playerDataModel.moveDir.z + transform.right * playerDataModel.moveDir.x) * playerDataModel.climbCheckLength, LayerMask.GetMask("Ground")))
        {
            if (!Physics.Raycast(transform.position + transform.up * playerDataModel.climbCheckHighHeight, (transform.forward * playerDataModel.moveDir.z + transform.right * playerDataModel.moveDir.x), playerDataModel.climbCheckLength, LayerMask.GetMask("Ground")))
            {
                playerDataModel.climable[playerDataModel.moveDir] = true;
                return;
            }
        }
        playerDataModel.climable[playerDataModel.moveDir] = false;
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float animatorSpeed = playerDataModel.animator.GetFloat("Move");
        if (playerDataModel.moveDir == Vector3.zero)
        {
            playerDataModel.rb.velocity = new Vector3(0f, playerDataModel.rb.velocity.y + Physics.gravity.y * Time.deltaTime, 0f);

            if (animatorSpeed > 0f)
            {
                playerDataModel.animator.SetFloat("Move", animatorSpeed - playerDataModel.moveSpeed * Time.deltaTime);
            }
            else if (animatorSpeed < 0f)
            {
                playerDataModel.animator.SetFloat("Move", 0f);
            }
        }
        else
        {
            if((playerDataModel.prevDir.x < 0f && playerDataModel.moveDir.x > 0f) || (playerDataModel.prevDir.x > 0f && playerDataModel.moveDir.x < 0f))
                playerDataModel.rb.velocity = new Vector3(0f, playerDataModel.rb.velocity.y + Physics.gravity.y * Time.deltaTime, playerDataModel.rb.velocity.z);
            if ((playerDataModel.prevDir.z < 0f && playerDataModel.moveDir.z > 0f) || (playerDataModel.prevDir.z > 0f && playerDataModel.moveDir.z < 0f))
                playerDataModel.rb.velocity = new Vector3(playerDataModel.rb.velocity.x, playerDataModel.rb.velocity.y + Physics.gravity.y * Time.deltaTime, 0f);

            if (animatorSpeed < 1f)
            {
                playerDataModel.animator.SetFloat("Move", animatorSpeed + playerDataModel.moveSpeed * Time.deltaTime);
            }
            else if (animatorSpeed > 1f)
            {
                playerDataModel.animator.SetFloat("Move", 1f);
            }
        }

        if (playerDataModel.animator.GetBool("IsGround"))
            playerDataModel.rb.AddForce(((transform.forward * playerDataModel.moveDir.z) + (transform.right * playerDataModel.moveDir.x)) * playerDataModel.moveSpeed, ForceMode.Force);
        else
            playerDataModel.rb.AddForce(((transform.forward * playerDataModel.moveDir.z) + (transform.right * playerDataModel.moveDir.x)) * playerDataModel.moveSpeed * 0.5f, ForceMode.Force);

        if (playerDataModel.rb.velocity.x > playerDataModel.highSpeed)
            playerDataModel.rb.velocity = new Vector3(playerDataModel.highSpeed, playerDataModel.rb.velocity.y + Physics.gravity.y * Time.deltaTime, playerDataModel.rb.velocity.z);
        if (playerDataModel.rb.velocity.z > playerDataModel.highSpeed)
            playerDataModel.rb.velocity = new Vector3(playerDataModel.rb.velocity.x, playerDataModel.rb.velocity.y + Physics.gravity.y * Time.deltaTime, playerDataModel.highSpeed);

        if (playerDataModel.climable.ContainsKey(playerDataModel.moveDir) && playerDataModel.climable[playerDataModel.moveDir])
        {
            playerDataModel.rb.AddForce(transform.up * playerDataModel.moveSpeed * 0.4f, ForceMode.Force);
        }
    }

    void Jump()
    {
        if (playerDataModel.isJump)
        {
            playerDataModel.rb.AddForce(transform.up * playerDataModel.jumpPower, ForceMode.Impulse);
            playerDataModel.isJump = false;
        }
    }

    void OnMove(InputValue inputValue)
    {
        playerDataModel.prevDir = playerDataModel.moveDir;
        Vector2 tmp = inputValue.Get<Vector2>();
        playerDataModel.moveDir = new Vector3(tmp.x, 0f, tmp.y);
    }

    void OnJump(InputValue inputValue)
    {
        if(playerDataModel.jumpCount < playerDataModel.jumpLimit)
        {
            playerDataModel.hero.Jump(inputValue.isPressed);
        }
    }
}
