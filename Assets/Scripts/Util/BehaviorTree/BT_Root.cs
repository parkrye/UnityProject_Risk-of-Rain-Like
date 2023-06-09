public class BT_Root : BT_Behavior
{
    BT_Behavior child;

    public BT_Root()
    {
        SetNodeType(NodeType.Root);
        SetParent(null);
    }

    public void AddChild(BT_Behavior _child)
    {
        child = _child;
        child.SetParent(this);
    }

    public BT_Behavior GetChild()
    {
        return child;
    }

    /// <summary>
    /// 자식 노드를 종료
    /// </summary>
    public override void Terminate()
    {
        child.Terminate();
    }

    public override NodeState Tick()
    {
        // 자식이 없다면 초기 상태임을 반환
        if (child == null)
            return NodeState.Invalid;
        // 자식의 상태가 초기 상태라면
        else if(child.GetState() == NodeState.Invalid)
        {
            // 자식 노드를 초기화하고
            child.Initialize();
            // 자식 노드 상태를 실행중으로 변경하고
            child.SetState(NodeState.Running);
        }
        // 자식 노드를 실행하고 그 상태를 자신의 상태에 저장
        SetState(child.Renew());

        // 자식의 상태를 자신의 상태로 저장해주고
        child.SetState(GetState());

        // 자신의 상태가 실행 중이라면
        if(GetState() != NodeState.Running)
        {
            // 자식 노드를 종료
            Terminate();
        }

        // 자신의 상태를 반환
        return GetState();
    }
}
