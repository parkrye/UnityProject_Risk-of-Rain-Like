/// <summary>
/// 자식 노드를 순차적으로 업데이트
/// 자식 노드가 실패한다면 다음 자식 노드 진행
/// 자식 노드가 성공한다면 다음 자식 노드는 진행하지 않음
/// </summary>
public class BT_Fallback : BT_Composite
{
    public BT_Fallback()
    {
        SetNodeType(NodeType.Fallback);
    }

    public override NodeState Renew()
    {
        for(int i = 0; i < GetChildrenCount(); i++)
        {
            // 각 자식 노드를 일단 업데이트
            NodeState currentState = GetChild(i).Tick();

            // 만약 자식 노드가 실패하지 않았다면
            if(currentState != NodeState.Failure)
            {
                // 해당 노드를 제외한 모든 자식 노드를 초기화
                ClearChild(i);
                // 해당 노드의 실행 결과를 반환
                return currentState;
            }
            // 실패했다면 다음 자식 노드로
            else
            {
                continue;
            }
        }

        // 모두 실패했다면 실패를 반환
        return NodeState.Failure;
    }

    /// <summary>
    /// 특정 자식을 제외한 모든 자식을 초기화
    /// </summary>
    /// <param name="skipIndex">초기화하지 않을 번호</param>
    protected void ClearChild(int skipIndex)
    {
        for(int i = 0; i < GetChildrenCount(); i++) 
        { 
            if(i != skipIndex)
            {
                GetChild(i).Reset();
            }
        }
    }
}
