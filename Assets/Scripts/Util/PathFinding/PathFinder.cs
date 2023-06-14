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
    public static Stack<Vector3> PathFindingForAerial(Transform start, Transform end, float distance)
    {
        Stack<Vector3> answer = new Stack<Vector3>();   // 반환 스택
        answer.Push(end.position);                      // 초기값 = 목표 좌표(플레이어 좌표)

        // 만약 플레이어와 에너미 사이에 벽이 있다면
        if (!CheckPassable(start.position, end.position, distance))
        {
            // x, y, z 거리 차이. 최소 1
            int xDiff = (int)Mathf.Abs(start.position.x - end.position.x);
            if (xDiff == 0) xDiff = 1;
            int yDiff = (int)Mathf.Abs(start.position.y - end.position.y);
            if (yDiff == 0) yDiff = 1;
            int zDiff = (int)Mathf.Abs(start.position.z - end.position.z);
            if (zDiff == 0) zDiff = 1;

            Dictionary<Vector3, bool> visited = new Dictionary<Vector3, bool>();    // 좌표, 노드 방문 여부 딕셔너리
            Dictionary<Vector3, Node> nodes = new Dictionary<Vector3, Node>();      // 좌표, 노드 딕셔너리
            PriorityQueue<Node, int> pq = new PriorityQueue<Node, int>();           // 총 예상 거리로 노드를 정렬한 우선순위 큐

            // 초기 노드를 저장
            Node startNode = new Node();
            startNode.position = start.position;
            nodes.Add(startNode.position, startNode);
            pq.Enqueue(startNode, 0);

            // 우선순위 큐에 노드가 있다면 계속 반복
            while(pq.Count > 0)
            {
                Node node = pq.Dequeue();               // 현재 노드
                if(!visited.ContainsKey(node.position)) // 만약 방문하지 않은 노드라면 방문 노드에 추가
                    visited.Add(node.position, true);

                // 종료 조건 1 : 목표 좌표와의 거리가 정지 거리 이하
                if (Vector3.Distance(node.position, end.position) <= distance)
                {
                    // 노드의 부모가 없을 때까지 반복
                    while (node.parent != null)
                    {
                        answer.Push(node.position); // 현재 노드의 좌표를 저장하고
                        if (nodes.ContainsKey(node.parent))
                            node = nodes[node.parent];  // 노드의 부모 노드로
                        else
                            break;
                    }
                    return answer;                  // 저장된 스택을 반환(목표 좌표 => 이전 좌표 => ... => 초기 좌표)
                }

                // 종료 조건 2 : 현재 좌표부터 목표 좌표 사이에 벽이 없다
                if (CheckPassable(node.position, end.position, distance))
                {
                    // 노드의 부모가 없을 때까지 반복
                    while (node.parent != null)
                    {
                        answer.Push(node.position); // 현재 노드의 좌표를 저장하고
                        node = nodes[node.parent];  // 노드의 부모 노드로
                    }
                    return answer;                  // 저장된 스택을 반환(목표 좌표 => 이전 좌표 => ... => 초기 좌표)
                }

                // 각 x, y, z로부터 -1 ~ +1 떨어진 좌표를 탐색
                for (int x = -1; x <= 1; x++)
                {
                    for(int y = -1; y <= 1; y++)
                    {
                        for(int  z = -1; z <= 1; z++)
                        {
                            // 0, 0, 0은 현재 좌표이므로 패스
                            if (x == y && y == z && z == 0)
                                continue;

                            // 현재 탐색한 좌표
                            Vector3 findPosition = node.position + xDiff * x * Vector3.right + yDiff * y * Vector3.up + zDiff * z * Vector3.forward;

                            // 이미 방문한 좌표라면 패스
                            if (visited.ContainsKey(findPosition))
                                continue;

                            int g = node.g + Mathf.Abs(x) + Mathf.Abs(y) + Mathf.Abs(z);    // 이동 거리 + 이동한 x, y, z 거리
                            int h = (int)Vector3.Distance(findPosition, end.position);      // 예상 거리 = 현재부터 목표꺼지 직선 거리

                            // 새 노드 생성
                            Node findNode = new Node(findPosition, node.position, g, h);
                            if (!nodes.ContainsKey(findPosition))           // 만약 새 노드가 처음 발견한 노드라면
                            {
                                nodes.Add(findPosition, findNode);          // 노드 목록에 새 노드를 추가하고
                                pq.Enqueue(findNode, findNode.f);           // 큐에 추가
                            }
                            else if(nodes[findPosition].f > findNode.f)     // 만약 새 노드가 기존에 있었으며, 기존보다 총 예상거리가 적다면
                            {
                                nodes[findPosition] = findNode;             // 노드 목록을 새 노드로 수정하고
                                pq.Enqueue(findNode, findNode.f);           // 큐에 추가
                            }
                        }
                    }
                }
            }
        }
        // 벽이 없었다면 그대로 반환
        else
        {
            return answer;
        }

        // 탐지 결과 길을 찾지 못했다면 플레이어의 뒤로 이동시키고
        TeleportToBackside(start, end, distance);
        return answer;  // 그대로 반환
    }

    /// <summary>
    /// 간단한 구조체
    /// 좌표, 부모(이 노드를 가리킨 노드의 좌표), 현재까지 거리, 예상되는 앞으로의 거리, 총 예상 거리를 갖음
    /// </summary>
    struct Node
    {
        public Vector3 position;
        public Vector3 parent;

        public int g;
        public int h;
        public int f;

        public Node(Vector3 _position, Vector3 _parent, int _g, int _h)
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
    static bool CheckPassable(Vector3 start, Vector3 end, float distance)
    {
        if (Physics.Raycast(start, end - start, distance, LayerMask.GetMask("Ground")))
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 플레이어의 뒤로 에너미를 이동시킨다
    /// </summary>
    /// <param name="enemy">에너미 위치</param>
    /// <param name="player">플레이어 위치</param>
    /// <param name="distance">에너미 정지 거리</param>
    static void TeleportToBackside(Transform enemy, Transform player, float distance)
    {
        RaycastHit hit;
        if(Physics.Raycast(player.position, -player.forward, out hit, distance, LayerMask.GetMask("Ground")))
        {
            enemy.position = hit.point;
        }
        else
        {
            enemy.position = player.position - player.forward * distance;
        }
    }
}
