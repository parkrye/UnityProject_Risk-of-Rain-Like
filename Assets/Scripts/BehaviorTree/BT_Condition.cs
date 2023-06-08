public class BT_Condition : BT_Behavior
{
    public BT_Condition()
    {
        SetNodeType(NodeType.Condition);
    }

    public override NodeState Tick()
    {
        // 조건을 확인
        SetState(Renew());

        if(GetState() == NodeState.Running)
        {
            // Renew의 결과는 반드시 success, failure 둘 중 하나임
        }

        // 조건에 대한 결과를 반환
        return GetState();
    }
}
