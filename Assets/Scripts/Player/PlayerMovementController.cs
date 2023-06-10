using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    PlayerDataModel playerDataModel;

    [SerializeField] Vector3 moveDir;
    [SerializeField] Dictionary<Vector3, bool> climable;
    [SerializeField] float animatorChangeSpeed;

    void Awake()
    {
        playerDataModel = GetComponent<PlayerDataModel>();
        climable = new Dictionary<Vector3, bool>();
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
        if (moveDir == Vector3.zero)
            return;

        if(!climable.ContainsKey(moveDir))
            climable.Add(moveDir, false);

        Debug.DrawRay(transform.position + transform.up * playerDataModel.climbCheckLowHeight, (transform.forward * moveDir.z + transform.right * moveDir.x) * playerDataModel.climbCheckLength, Color.red, 2f);
        Debug.DrawRay(transform.position + transform.up * playerDataModel.climbCheckHighHeight, (transform.forward * moveDir.z + transform.right * moveDir.x) * playerDataModel.climbCheckLength, Color.red, 2f);
        if (Physics.Raycast(transform.position + transform.up * playerDataModel.climbCheckLowHeight, (transform.forward * moveDir.z + transform.right * moveDir.x) * playerDataModel.climbCheckLength, LayerMask.GetMask("Ground")))
        {
            if (!Physics.Raycast(transform.position + transform.up * playerDataModel.climbCheckHighHeight, (transform.forward * moveDir.z + transform.right * moveDir.x), playerDataModel.climbCheckLength, LayerMask.GetMask("Ground")))
            {
                climable[moveDir] = true;
                return;
            }
        }
        climable[moveDir] = false;
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float animatorFoward = playerDataModel.animator.GetFloat("Foward");
        float animatorSide = playerDataModel.animator.GetFloat("Side");

        // 이동
        playerDataModel.rb.velocity = (transform.right * moveDir.x + transform.forward * moveDir.z) * playerDataModel.moveSpeed + transform.up * (playerDataModel.rb.velocity.y + Physics.gravity.y * Time.deltaTime);
        
        // 등반
        if (climable.ContainsKey(moveDir) && climable[moveDir])
        {
            playerDataModel.rb.AddForce(transform.up * playerDataModel.moveSpeed * Time.deltaTime, ForceMode.Force);
        }

        // 애니메이션
        if (moveDir.z > 0f)
        {
            if (animatorFoward < 1f)
            {
                playerDataModel.animator.SetFloat("Foward", animatorFoward + animatorChangeSpeed);
            }
            else
            {
                playerDataModel.animator.SetFloat("Foward", 1f);
            }
        }
        else if (moveDir.z < 0f)
        {
            if (animatorFoward > -1f)
            {
                playerDataModel.animator.SetFloat("Foward", animatorFoward - animatorChangeSpeed);
            }
            else
            {
                playerDataModel.animator.SetFloat("Foward", -1f);
            }
        }
        else
        {
            if (animatorFoward >= 0.1f)
            {
                playerDataModel.animator.SetFloat("Foward", animatorFoward - animatorChangeSpeed);
            }
            else if (animatorFoward <= -0.1f)
            {
                playerDataModel.animator.SetFloat("Foward", animatorFoward + animatorChangeSpeed);
            }
            else
            {
                playerDataModel.animator.SetFloat("Foward", 0f);
            }
        }

        if (moveDir.x > 0f)
        {
            if (animatorSide < 1f)
            {
                playerDataModel.animator.SetFloat("Side", animatorSide + animatorChangeSpeed);
            }
            else
            {
                playerDataModel.animator.SetFloat("Side", 1f);
            }
        }
        else if (moveDir.x < 0f)
        {
            if (animatorSide > -1f)
            {
                playerDataModel.animator.SetFloat("Side", animatorSide - animatorChangeSpeed);
            }
            else
            {
                playerDataModel.animator.SetFloat("Side", -1f);
            }
        }
        else
        {
            if (animatorSide >= 0.1f)
            {
                playerDataModel.animator.SetFloat("Side", animatorSide - animatorChangeSpeed);
            }
            else if (animatorSide <= -0.1f)
            {
                playerDataModel.animator.SetFloat("Side", animatorSide + animatorChangeSpeed);
            }
            else
            {
                playerDataModel.animator.SetFloat("Side", 0f);
            }
        }
    }

    void Jump()
    {
        if (playerDataModel.isJump)
        {
            playerDataModel.rb.velocity += transform.up * playerDataModel.jumpPower;
            playerDataModel.isJump = false;
        }
    }

    void OnMove(InputValue inputValue)
    {
        Vector2 tmp = inputValue.Get<Vector2>();
        moveDir = new Vector3(tmp.x, 0f, tmp.y);
    }

    void OnJump(InputValue inputValue)
    {
        if(playerDataModel.jumpCount < playerDataModel.jumpLimit)
        {
            playerDataModel.hero.Jump(inputValue.isPressed);
        }
    }
}
