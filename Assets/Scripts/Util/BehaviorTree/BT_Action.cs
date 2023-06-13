using UnityEngine;

public class BT_Action : BT_Behavior
{
    public BT_Action()
    {
        SetNodeType(NodeType.Action);
    }

    public override void Initialize()
    {

    }

    public override void Terminate()
    {

    }

    public override void Reset()
    {
        SetState(NodeState.Invalid);
    }

    public override NodeState Tick()
    {
        // 현재 상태가 초기 상태라면
        if(GetState() == NodeState.Invalid)
        {
            // 초기화
            Initialize();
            // 상태를 실행으로
            SetState(NodeState.Running);
        }

        // 실행하고 그 상태를 저장
        SetState(Renew());

        // 현재 상태가 아직도 진행중이라면
        if(GetState() != NodeState.Running) 
        { 
            // 종료
            Terminate();
        }

        // 현재 상태 반환
        return GetState();
    }
}
