using System.Collections;
using System.Collections.Generic;
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
    }

    /// <summary>
    /// 원형을 그리는 메소드
    /// </summary>
    void DrawCircle()
    {
        // 선분 개수 지정
        circleRenderer.positionCount = steps + 1;

        // 각 선분마다
        for(int currentStep = 0; currentStep < steps; currentStep++)
        {
            // 몇 번째 선분인지(비율)
            float circumferenceProgress = (float)currentStep / steps;

            // 원의 둘레 중 현재 비율
            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            // 현재 비율에서 x, y의 위치
            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            // 실제 그려야 할 위치
            float x = xScaled * radius;
            float y = yScaled * radius;

            // 그려야 할 위치 좌표화
            Vector3 currentPosition = new Vector3(x, 0f, y);

            // 선분을 그린다
            circleRenderer.SetPosition(currentStep, target + currentPosition);
        }
    }
}
