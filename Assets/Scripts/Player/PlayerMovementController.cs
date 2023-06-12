using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    PlayerDataModel playerDataModel;

    public Vector3 moveDir { get; private set; }
    Vector3 prevDir;
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
        SetDirection();
    }

    void CheckGround()
    {
        if (!playerDataModel.isJump)
        {
            if (Physics.Raycast(transform.position + transform.up * 0.1f, -transform.up, 0.2f, LayerMask.GetMask("Ground")))
            {
                playerDataModel.hero.Jump(false);
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

    void SetDirection()
    {
        prevDir = moveDir;
    }

    void FixedUpdate()
    {
        if (playerDataModel.controlleable)
        {
            Move();
        }
    }

    void Move()
    {
        float animatorFoward = playerDataModel.animator.GetFloat("Foward");
        float animatorSide = playerDataModel.animator.GetFloat("Side");

        // 제동
        if(moveDir == Vector3.zero)
        {
            playerDataModel.rb.velocity = new Vector3(0f, playerDataModel.rb.velocity.y, 0f);
        }
        else
        {
            if((prevDir.x < 0f && moveDir.x >= 0f) || (prevDir.x > 0f && moveDir.x <= 0f))
            {
                playerDataModel.rb.velocity = new Vector3(0f, playerDataModel.rb.velocity.y, playerDataModel.rb.velocity.z);
            }
            if((prevDir.z < 0f && moveDir.z >= 0f) || (prevDir.z > 0f && moveDir.z <= 0f))
            {
                playerDataModel.rb.velocity = new Vector3(playerDataModel.rb.velocity.x, playerDataModel.rb.velocity.y, 0f);
            }
        }
        if (playerDataModel.rb.velocity.x > playerDataModel.highSpeed)
            playerDataModel.rb.velocity = new Vector3(playerDataModel.highSpeed, playerDataModel.rb.velocity.y, playerDataModel.rb.velocity.z);
        if (playerDataModel.rb.velocity.z > playerDataModel.highSpeed)
            playerDataModel.rb.velocity = new Vector3(playerDataModel.rb.velocity.x, playerDataModel.rb.velocity.y, playerDataModel.highSpeed);

        // 이동
        if (playerDataModel.animator.GetBool("IsGround"))
            playerDataModel.rb.AddForce(((transform.right * moveDir.x) + (transform.forward * moveDir.z)) * playerDataModel.moveSpeed, ForceMode.Force);
        else
            playerDataModel.rb.AddForce(((transform.right * moveDir.x) + (transform.forward * moveDir.z)) * Mathf.Sqrt(playerDataModel.moveSpeed), ForceMode.Force);

        // 등반
        if (climable.ContainsKey(moveDir) && climable[moveDir])
        {
            playerDataModel.rb.AddForce(transform.up * playerDataModel.climbPower, ForceMode.Force);
        }

        // 애니메이션
        playerDataModel.animator.SetFloat("Foward", Mathf.Lerp(animatorFoward, moveDir.z, 0.1f));
        playerDataModel.animator.SetFloat("Side", Mathf.Lerp(animatorSide, moveDir.x, 0.1f));
    }

    void OnMove(InputValue inputValue)
    {
        Vector2 tmp = inputValue.Get<Vector2>();
        moveDir = new Vector3(tmp.x, 0f, tmp.y);
    }

    void OnJump(InputValue inputValue)
    {
        playerDataModel.hero.Jump(inputValue.isPressed);
    }
}
