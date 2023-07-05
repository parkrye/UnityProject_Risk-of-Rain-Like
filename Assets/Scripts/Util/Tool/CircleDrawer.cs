using UnityEngine;

public class CircleDrawer : MonoBehaviour
{
    LineRenderer circleRenderer;
    int steps;
    float radius;
    Vector3 target;

    void Awake()
    {
        circleRenderer = GetComponent<LineRenderer>();
    }

    public void Setting(Vector3 _target, int _stpes, float _radius)
    {
        target = _target;
        steps = _stpes;
        radius = _radius;

        DrawCircle();
    }

    /// <summary>
    /// 원형을 그리는 메소드
    /// </summary>
    void DrawCircle()
    {
        // 각도
        float angle = 0f;

        // 렌더러의 처음과 끝을 이어준다
        circleRenderer.loop = true;

        // 선분 개수 지정
        circleRenderer.positionCount = steps;
        float reverseStep = 1 / steps;

        // 각 선분마다
        for (int currentStep = 0; currentStep < steps; currentStep++)
        {
            // 삼각함수로 angle에 대한 x, y 위치를 지정
            float x = radius * Trigonometrics.Cos(angle);
            float y = radius * Trigonometrics.Sin(angle);

            circleRenderer.SetPosition(currentStep, target + new Vector3(x, 0f, y));

            angle += 360f * reverseStep;
        }
    }
}
