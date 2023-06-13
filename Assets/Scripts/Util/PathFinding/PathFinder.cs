using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public static Vector3 PathFinding(Transform start, Transform end, float distance)
    {
        if(!CheckPassable(start.position, end.position, distance))
        {
            for(int modifier = 1; modifier < 100; modifier++)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        for(int k = -1; k <= 1; k++)
                        {
                            if (i == j && j == k && k == i)
                                continue;
                            if (CheckPassable(start.position + (Vector3.right * i + Vector3.up * j + Vector3.forward * k) * modifier, end.position, distance))
                            {
                                return start.position + (Vector3.right * i + Vector3.up * j + Vector3.forward * k) * modifier;
                            }
                        }
                    }
                }
            }
        }

        return end.position;
    }

    static bool CheckPassable(Vector3 start, Vector3 end, float distance)
    {
        if (Physics.Raycast(start, end - start, distance, LayerMask.GetMask("Ground")))
        {
            return false;
        }
        return true;
    }
}
