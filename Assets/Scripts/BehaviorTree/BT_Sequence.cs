/// <summary>
/// 자식 노드를 순차적으로 업데이트
/// 자식 노드가 성공한다면 다음 자식 노드 진행
/// 자식 노드가 실패한다면 다음 자식 노드는 진행하지 않음
/// </summary>
public class BT_Sequence : BT_Composite
{
    public BT_Sequence()
    {
        SetNodeType(NodeType.Sequence);
    }

    public override NodeState Renew()
    {
        NodeState currentState = NodeState.Invalid;

        for(int i = 0; i < GetChildrenCount(); i++)
        {
            // 현재 자식의 상태
            currentState = GetChild(i).GetState();

            // 현재 자식 노드가 행동이 아니거나, (행동이라면) 현재 자식 노드의 상태가 성공이 아니라면
            if(GetChild(i).GetNodeType() != NodeType.Action || GetChild(i).GetState() != NodeState.Success)
            {
                // 현재 자식 노드를 업데이트
                currentState = GetChild(i).Tick();
            }

            // 업데이트 후 현재 자식 노드가 성공하지 않았다면
            if(currentState != NodeState.Success)
            {
                // 현재 노드 상태를 반환
                return currentState;
            }
            // 성공했다면 다음 자식 노드로
            else
            {
                continue;
            }
        }

        // 모든 자식 노드가 성공이라면 성공을 반환
        return NodeState.Success;
    }
}
