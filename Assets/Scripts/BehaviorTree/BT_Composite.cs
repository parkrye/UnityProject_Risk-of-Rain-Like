using System.Collections.Generic;

/// <summary>
/// 제어 흐름 노드
/// </summary>
public class BT_Composite : BT_Behavior
{
    protected List<BT_Behavior> children;

    public BT_Composite()
    {
        children = new List<BT_Behavior>();
    }

    /// <summary>
    /// 모든 노드 상태 초기화
    /// </summary>
    public override void Reset()
    {
        foreach (var child in children)
        {
            child.Reset();
        }
    }

    public BT_Behavior GetChild(int index)
    {
        return children[index];
    }

    public int GetChildrenCount()
    {
        return children.Count;
    }

    public void AddChild(BT_Behavior _node)
    {
        children.Add(_node);
        _node.SetIndex(children.Count - 1);
        _node.SetParent(this);
    }
}
