using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    PlayerDataModel playerDataModel;
    [SerializeField] PlayerGroundChecker groundChecker;

    public Vector3 moveDir, dirModifier;
    [SerializeField] Vector3 curVelocity;
    [SerializeField] float slopeDegree;
    [SerializeField] bool isSlope;
    [SerializeField] AudioSource jumpAudio;
    RaycastHit slopeHit;

    void Awake()
    {
        playerDataModel = GetComponent<PlayerDataModel>();
    }

    void Update()
    {
        if (!playerDataModel.onESC)
        {
            CheckGround();
            CheckSlope();
        }
    }

    void CheckGround()
    {
        if (groundChecker.IsGround)
        {
            playerDataModel.jumpCount = playerDataModel.jumpLimit;
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
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);   // 상향 벡터와 바닥면의 법선 벡터 사이의 각에 대하여
            isSlope = angle != 0f && angle <= slopeDegree;              // 각이 0이 아니고(평평), 정해둔 경사 각도 이하라면 경사면
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
        if (!playerDataModel.onESC)
        {
            Move();
        }
    }

    void Move()
    {
        float animatorFoward = playerDataModel.animator.GetFloat("Foward");
        float animatorSide = playerDataModel.animator.GetFloat("Side");

        // 경사로 보정
        float gravity = (playerDataModel.rb.velocity.y < 0 ? -playerDataModel.rb.velocity.y : playerDataModel.rb.velocity.y) - Physics.gravity.y * Time.deltaTime * playerDataModel.TimeScale;
        if(groundChecker.IsGround && isSlope)
        {
            curVelocity = AdjustDirectionToSlope(moveDir);
            gravity = 0f;
        }
        else
            curVelocity = moveDir;
        curVelocity = transform.right * curVelocity.x + transform.forward * curVelocity.z;

        // 이동
        Debug.Log(gravity);
        playerDataModel.rb.velocity = playerDataModel.MoveSpeed * playerDataModel.TimeScale * curVelocity + Vector3.down * gravity + dirModifier;
        if (Vector3.SqrMagnitude(dirModifier - Vector3.zero) > 0.1f)
            dirModifier = Vector3.Lerp(dirModifier, Vector3.zero, Time.deltaTime * 5f);

        // 애니메이션
        playerDataModel.animator.SetFloat("Foward", Mathf.Lerp(animatorFoward, moveDir.z, Time.deltaTime * playerDataModel.TimeScale * 5f));
        playerDataModel.animator.SetFloat("Side", Mathf.Lerp(animatorSide, moveDir.x, Time.deltaTime * playerDataModel.TimeScale * 5f));
    }

    void OnMove(InputValue inputValue)
    {
        Vector2 tmp = inputValue.Get<Vector2>();

        if (playerDataModel.controllable)
            moveDir = new Vector3(tmp.x, 0f, tmp.y);
        else
            moveDir = Vector3.zero;
    }

    void OnJump(InputValue inputValue)
    {
        if (playerDataModel.controllable)
        {
            if (playerDataModel.jumpCount > 0)
            {
                if (playerDataModel.hero.Jump(inputValue.isPressed))
                {
                    jumpAudio.Play();
                    if(groundChecker.IsGround)
                        groundChecker.JumpReady();
                }
            }
            else
            {
                playerDataModel.hero.Jump(false);
            }
        }
    }
}
