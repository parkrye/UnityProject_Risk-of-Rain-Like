using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    /// <summary>
    /// 간단한 공중 길찾기
    /// </summary>
    /// <param name="start">시작 좌표</param>
    /// <param name="end">종료 좌표</param>
    /// <param name="distance">정지 거리</param>
    /// <returns>end가 가장 위에 위치한 이동 좌표 스택</returns>
    public static Stack<Vector3> PathFindingForAerial(Vector3 start, Vector3 end)
    {
        Stack<Vector3> answer = new Stack<Vector3>();   // 반환 스택
        answer.Push(end);     // 초기값 = 목표 좌표(플레이어 좌표)

        Dictionary<Vector3, bool> visited = new Dictionary<Vector3, bool>();    // 좌표, 노드 방문 여부 딕셔너리
        Dictionary<Vector3, Node> nodes = new Dictionary<Vector3, Node>();      // 좌표, 노드 딕셔너리
        PriorityQueue<Node, float> pq = new PriorityQueue<Node, float>();           // 총 예상 거리로 노드를 정렬한 우선순위 큐

        // 초기 노드를 저장
        Node startNode = new Node();
        startNode.position = start;
        nodes.Add(startNode.position, startNode);
        pq.Enqueue(startNode, 0);

        // 우선순위 큐에 노드가 있다면 반복
        while (pq.Count > 0)
        {
            Node node = pq.Dequeue();                // 현재 노드
            if (!visited.ContainsKey(node.position)) // 만약 방문하지 않은 노드라면 방문 노드에 추가
                visited.Add(node.position, true);
            else
                continue;

            // 종료 조건 : 현재 좌표부터 목표 좌표 사이에 벽이 없다
            if (CheckPassable(node.position, end))
            {
                // 노드의 부모가 없을 때까지 반복
                while (nodes.ContainsKey(node.parent))
                {
                    answer.Push(node.position); // 현재 노드의 좌표를 저장하고
                    node = nodes[node.parent];  // 노드의 부모 노드로
                }
                return answer;                  // 저장된 스택을 반환(목표 좌표 => 이전 좌표 => ... => 초기 좌표)
            }

            // => 플레이어 방향 좌표만 탐색하도록 개선
            // 각 x, y, z로부터 -1 ~ +1 떨어진 좌표를 탐색
            for (float x = -1f; x <= 1f; x += 1f)
            {
                for (float y = -1f; y <= 1f; y += 1f)
                {
                    for (float z = -1f; z <= 1f; z += 1f)
                    {
                        // 0, 0, 0은 현재 좌표이므로 패스
                        if (x == y && y == z && z == 0f)
                            continue;

                        // 현재 탐색한 좌표
                        Vector3 findPosition = node.position + x * Vector3.right + y * Vector3.up + z * Vector3.forward;

                        // 이미 방문한 좌표라면 패스
                        if (visited.ContainsKey(findPosition))
                            continue;

                        // 사이에 벽이 있다면 패스
                        if (!CheckPassable(node.position, findPosition))
                            continue;

                        float g = node.g + Mathf.Sqrt(x * x + y * y + z * z);    // 이동 거리 + 이동한 거리
                        float h = Vector3.Distance(findPosition, end);      // 예상 거리 = 현재부터 목표꺼지 직선 거리

                        // 새 노드 생성
                        Node findNode = new Node(findPosition, node.position, g, h);
                        if (!nodes.ContainsKey(findPosition))           // 만약 새 노드가 처음 발견한 노드라면
                        {
                            nodes.Add(findPosition, findNode);          // 노드 목록에 새 노드를 추가하고
                            pq.Enqueue(findNode, findNode.f);           // 큐에 추가
                        }
                        else if (nodes[findPosition].f > findNode.f)     // 만약 새 노드가 기존에 있었으며, 기존보다 총 예상거리가 적다면
                        {
                            nodes[findPosition] = findNode;             // 노드 목록을 새 노드로 수정하고
                            pq.Enqueue(findNode, findNode.f);           // 큐에 추가
                        }
                    }
                }
            }
        }

        // 길을 찾지 못했다면 그대로 반환
        return answer;
    }

    /// <summary>
    /// 간단한 구조체
    /// 좌표, 부모(이 노드를 가리킨 노드의 좌표), 현재까지 거리, 예상되는 앞으로의 거리, 총 예상 거리를 갖음
    /// </summary>
    struct Node
    {
        public Vector3 position;
        public Vector3 parent;

        public float g;
        public float h;
        public float f;

        public Node(Vector3 _position, Vector3 _parent, float _g, float _h)
        {
            position = _position;
            parent = _parent;
            g = _g;
            h = _h;
            f = g + h;
        }
    }

    /// <summary>
    /// 간단한 벽 감지
    /// </summary>
    /// <param name="start">현재 좌표</param>
    /// <param name="end">목표 좌표</param>
    /// <param name="distance">감지 거리</param>
    /// <returns>감지 거리 이내에 벽이 있다면 false, 없다면 true</returns>
    static bool CheckPassable(Vector3 start, Vector3 end)
    {
        Ray ray = new Ray(start, (end - start).normalized);
        if (Physics.Raycast(ray, Vector3.Distance(start, end), LayerMask.GetMask("Ground")))
        {
            return false;
        }
        return true;
    }
}
