using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    PlayerDataModel playerDataModel;

    public Vector3 moveDir, dirModifier;
    [SerializeField] Vector3 curVelocity;
    [SerializeField] float slopeDegree;
    [SerializeField] bool isSlope;
    RaycastHit slopeHit;
    bool descending;

    void Awake()
    {
        playerDataModel = GetComponent<PlayerDataModel>();
    }

    void Update()
    {
        CheckGround();
        CheckSlope();
    }

    void CheckGround()
    {
        if (playerDataModel.rb.velocity.y > 0f)
        {
            descending = true;
        }

        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
        if (Physics.Raycast(ray, 0.2f, LayerMask.GetMask("Ground")))
        {
            if (descending && playerDataModel.rb.velocity.y < 0f)
            {
                playerDataModel.jumpCount = 0;
                descending = false;
            }

            playerDataModel.animator.SetBool("IsGround", true);
            return;
        }
        playerDataModel.animator.SetBool("IsGround", false);
    }

    /// <summary>
    /// 바닥면의 법선 벡터를 이용하여 현재 서있는 바닥이 경사로인지 확인하는 메소드
    /// </summary>
    void CheckSlope()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
        if(Physics.Raycast(ray, out slopeHit, 0.2f, LayerMask.GetMask("Ground")))
        {
            var angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            isSlope = angle != 0f && angle < slopeDegree;
            return;
        }
        isSlope = false;
    }

    /// <summary>
    /// 경사로의 지형 평면을 구하고 필요한 벡터 방향 정보를 반환하는 메소드
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    Vector3 AdjustDirectionToSlope(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float animatorFoward = playerDataModel.animator.GetFloat("Foward");
        float animatorSide = playerDataModel.animator.GetFloat("Side");

        // 경사로 보정
        float gravity = Mathf.Abs(playerDataModel.rb.velocity.y) - Physics.gravity.y * Time.deltaTime;
        if(playerDataModel.animator.GetBool("IsGround") && isSlope)
        {
            curVelocity = AdjustDirectionToSlope(moveDir);
            gravity = 0f;
        }
        else
            curVelocity = moveDir;
        if (!playerDataModel.rb.useGravity)
            gravity = 0f;
        curVelocity = transform.right * curVelocity.x + transform.forward * curVelocity.z;

        // 이동
        playerDataModel.rb.velocity = new Vector3(curVelocity.x, 0f, curVelocity.z) * playerDataModel.moveSpeed + Vector3.down * gravity + dirModifier;
        if (dirModifier != Vector3.zero)
            dirModifier = Vector3.Lerp(dirModifier, Vector3.zero, 0.1f);

        // 애니메이션
        playerDataModel.animator.SetFloat("Foward", Mathf.Lerp(animatorFoward, moveDir.z, 0.1f));
        playerDataModel.animator.SetFloat("Side", Mathf.Lerp(animatorSide, moveDir.x, 0.1f));
    }

    void OnMove(InputValue inputValue)
    {
        Vector2 tmp = inputValue.Get<Vector2>();

        if (playerDataModel.controlleable)
            moveDir = new Vector3(tmp.x, 0f, tmp.y);
        else
            moveDir = Vector3.zero;
    }

    void OnJump(InputValue inputValue)
    {
        if (playerDataModel.controlleable)
        {
            if (playerDataModel.jumpCount < playerDataModel.jumpLimit)
            {
                playerDataModel.hero.Jump(inputValue.isPressed);
            }
            else
            {
                playerDataModel.hero.Jump(false);
            }
        }
    }
}
