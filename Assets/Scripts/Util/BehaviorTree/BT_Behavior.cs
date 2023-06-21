// 행동 노드의 상태
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    Invalid, // 기본 상태
    Success, // 실행 성공
    Failure, // 실행 실패
    Running, // 실행 중임
    Aborted, // 오류 발생
}

// 노드의 역할
public enum NodeType
{
    Root,       // 루트 노드
    Fallback,   // 자식 노드를 순회. 하나라도 성공하면 즉시 성공 반환. 모두 실패하면 실패 반환. Or 조건
    Sequence,   // 자식 노드를 순회. 하나라도 실패하면 즉시 실패 반환. 모두 성공하면 성공 반환. And 조건
    Condition,  // 조건이 참일 경우 자식 노드를 실행
    Action,     // 행동 노드
}

public class BT_Behavior
{
    NodeState state;
    NodeType type;
    int index;
    BT_Behavior parent;
    protected GameObject enemy, player;

    public BT_Behavior()
    {
        state = NodeState.Invalid;
    }

    public void SetParent(BT_Behavior node)
    {
        parent = node;
    }

    public BT_Behavior GetParent()
    {
        return parent;
    }

    public NodeState GetState()
    {
        return state;
    }

    public void SetState(NodeState _state)
    {
        state = _state;
    }

    public NodeType GetNodeType()
    {
        return type;
    }

    public void SetNodeType(NodeType _type)
    {
        type = _type;
    }

    public int GetIndex()
    {
        return index;
    }

    public void SetIndex(int _index)
    {
        index = _index;
    }

    /// <summary>
    /// 노드 상태 초기화
    /// </summary>
    virtual public void Reset()
    {
        state = NodeState.Invalid;
    }

    /// <summary>
    /// 노드 초기화
    /// </summary>
    public virtual void Initialize()
    {

    }

    /// <summary>
    /// 노드 실행
    /// </summary>
    /// <returns>실행 후 상태</returns>
    public virtual NodeState Renew()
    {
        return NodeState.Success;
    }

    /// <summary>
    /// 노드 종료
    /// </summary>
    public virtual void Terminate()
    {

    }

    /// <summary>
    /// 업데이트 처리
    /// </summary>
    /// <returns></returns>
    public virtual NodeState Tick()
    {
        // 노드가 초기 상태라면
        if(state == NodeState.Invalid)
        {
            // 초기화
            Initialize();
            // 상태를 실행으로
            state = NodeState.Running;
        }

        // 노드를 실행하고 반환된 상태 저장
        state = Renew();

        // 상태가 실행중이 아니라면
        if(state != NodeState.Running)
        {
            // 종료
            Terminate();
        }

        // 노드 상태 반환
        return state;
    }
}
