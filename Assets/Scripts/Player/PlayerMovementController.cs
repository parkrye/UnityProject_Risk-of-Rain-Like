using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어 캐릭터 이동에 대한 스크립트
/// </summary>
public class PlayerMovementController : MonoBehaviour
{
    PlayerDataModel playerDataModel;                        // 플레이어 데이터 모델
    [SerializeField] PlayerGroundChecker groundChecker;     // 지면 접촉 판정자

    public Vector3 moveDir, dirModifier;                    // 이동 방향, 이동 조정자
    [SerializeField] Vector3 curMoveVelocity, moveVelocity; // 현재 이동 방향
    [SerializeField] float slopeDegree, gravity;            // 오를 수 있는 경사면 기울기, 중력값
    [SerializeField] bool isSlope;                          // 현재 경사면 위에 있는지
    [SerializeField] AudioSource jumpAudio;                 // 점프시 오디오
    RaycastHit slopeHit;                                    // 경사면 검사용 레이케스트 충돌

    void Awake()
    {
        playerDataModel = GetComponent<PlayerDataModel>();
    }

    void Update()
    {
        if (!playerDataModel.onESC) // ESC UI가 없다면
        {
            CheckGround();          // 지면 검사
            CheckSlope();           // 경사면 검사
            MoveCalculator();       // 이동 연산
        }
    }

    /// <summary>
    /// 지면 검사 메소드
    /// </summary>
    void CheckGround()
    {
        // 지면 검사기로부터 지면에 있다고 판단되면
        if (groundChecker.IsGround)
        {
            playerDataModel.jumpCount = playerDataModel.jumpLimit;  // 점프 카운트를 초기화하고
            playerDataModel.animator.SetBool("IsGround", true);     // 애니메이션을 지상 상태로 설정
            return;
        }
        playerDataModel.animator.SetBool("IsGround", false);        // 아니라면 애니메이션을 공중 상태로 설정
    }

    /// <summary>
    /// 바닥면의 법선 벡터를 이용하여 현재 서있는 바닥이 경사로인지 확인하는 메소드
    /// </summary>
    void CheckSlope()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);    // 사용 레이. 피봇보다 살짝 위로부터 아래로
        if(Physics.Raycast(ray, out slopeHit, 0.2f, LayerMask.GetMask("Ground")))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);   // 상향 벡터와 바닥면의 법선 벡터 사이의 각에 대하여
            isSlope = angle != 0f && angle <= slopeDegree;              // 각이 0이 아니고, 정해둔 경사 각도 이하라면 경사면에 서있다
            return;
        }
        isSlope = false;                                                // 각이 0이거나(평지), 정해둔 경사 각도 초과라면 경사면에 서있지 않다
    }

    /// <summary>
    /// 경사로의 지형 평면을 구하고 필요한 벡터 방향 정보를 반환하는 메소드
    /// </summary>
    /// <param name="direction">이동 벡터</param>
    /// <returns>경사면으로 투영된 정규화 벡터</returns>
    Vector3 AdjustDirectionToSlope(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    void MoveCalculator()
    {
        // 경사로 보정
        // 중력 = 현재 y 속도의 절대값 + 중력
        gravity = (playerDataModel.rb.velocity.y < 0 ? -playerDataModel.rb.velocity.y : playerDataModel.rb.velocity.y) - Physics.gravity.y * Time.deltaTime * playerDataModel.TimeScale;

        // 경사면 위에 있다면
        if (groundChecker.IsGround && isSlope)
        {
            curMoveVelocity = AdjustDirectionToSlope(moveDir);   // 현재 이동 방향을 경사면에 투영시키고
            gravity = 0f;                                        // 중력은 0으로
        }
        else
            curMoveVelocity = moveDir;                           // 그렇지 않다면 현재 이동 방향은 이동 방향 그대로

        // 현재 이동 방향에서 y값은 제거하고, 플레이어 방향, 속도와 연동
        curMoveVelocity = playerDataModel.MoveSpeed * (transform.right * curMoveVelocity.x + transform.forward * curMoveVelocity.z);

        // 이동 속력, 중력, 이동 조정치 적용
        moveVelocity = curMoveVelocity + Vector3.down * gravity + dirModifier;

        // 이동 조정치는 점점 0에 가깝게 감소
        if (Vector3.SqrMagnitude(dirModifier) > 0f)
            dirModifier = Vector3.Lerp(dirModifier, Vector3.zero, Time.deltaTime * 5f);

        // 애니메이션 값 설정
        playerDataModel.animator.SetFloat("Foward", Mathf.Lerp(playerDataModel.animator.GetFloat("Foward"), moveDir.z, Time.deltaTime * playerDataModel.TimeScale * 5f));
        playerDataModel.animator.SetFloat("Side", Mathf.Lerp(playerDataModel.animator.GetFloat("Side"), moveDir.x, Time.deltaTime * playerDataModel.TimeScale * 5f));
    }

    void FixedUpdate()
    {
        if (!playerDataModel.onESC) // ESC UI가 없다면
        {
            Move();                 // 이동한다
        }
    }

    /// <summary>
    /// 이동 메소드
    /// </summary>
    void Move()
    {
        playerDataModel.rb.velocity = moveVelocity;
    }

    /// <summary>
    /// 이동 입력에 대한 메소드
    /// </summary>
    /// <param name="inputValue"></param>
    void OnMove(InputValue inputValue)
    {
        Vector2 tmp = inputValue.Get<Vector2>();

        // 플레이어가 입력 가능한 상태라면, Vector3의 x,z로 저장
        if (playerDataModel.controllable)
            moveDir = new Vector3(tmp.x, 0f, tmp.y);
        // 아니라면 제로 벡터
        else
            moveDir = Vector3.zero;
    }

    /// <summary>
    /// 점프 입력에 대한 메소드
    /// </summary>
    /// <param name="inputValue"></param>
    void OnJump(InputValue inputValue)
    {
        // 일단 플레이어가 입력 가능한 상태일 때
        if (playerDataModel.controllable)
        {
            // 점프 가능 횟수가 남아있다면
            if (playerDataModel.jumpCount > 0)
            {
                // 일단 점프
                if (playerDataModel.hero.Jump(inputValue.isPressed))
                {
                    jumpAudio.Play();               // 오디오를 출력하고
                    if(groundChecker.IsGround)      // 지면에 있었다면
                        groundChecker.JumpReady();  // 점프 대기를 호출
                }
            }
            else
                playerDataModel.hero.Jump(false);   // 남아있지 않다면 false 점프
        }
    }
}
