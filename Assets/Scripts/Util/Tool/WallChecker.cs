using UnityEngine;

public class WallChecker : MonoBehaviour
{
    public bool isWall;

    public void MoveChecker(Vector3 pos)
    {
        transform.position = pos;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isWall = true;
        }
        else
        {
            isWall = false;
        }
    }
}
