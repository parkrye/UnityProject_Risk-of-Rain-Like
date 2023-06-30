using System.Collections;
using UnityEngine;

public class PositionFixer : MonoBehaviour
{
    public enum FixPos { None, X, Y, Z, XY, XZ, YZ, XYZ }
    public FixPos fixPos;
    public Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;

        StartCoroutine(FixRoutine());
    }

    IEnumerator FixRoutine()
    {
        Vector3 pos;
        while (target)
        {
            pos = Vector3.zero;
            switch (fixPos)
            {
                default:
                case FixPos.None:
                    break;
                case FixPos.X:
                    pos.x = target.position.x;
                    break;
                case FixPos.Y:
                    pos.y = target.position.y;
                    break;
                case FixPos.Z:
                    pos.z = target.position.z;
                    break;
                case FixPos.XY:
                    pos.x = target.position.x;
                    pos.y = target.position.y;
                    break;
                case FixPos.XZ:
                    pos.x = target.position.x;
                    pos.z = target.position.z;
                    break;
                case FixPos.YZ:
                    pos.y = target.position.y;
                    pos.z = target.position.z;
                    break;
                case FixPos.XYZ:
                    pos.x = target.position.x;
                    pos.y = target.position.y;
                    pos.z = target.position.z;
                    break;
            }
            transform.position = pos;
            yield return new WaitForFixedUpdate();
        }
    }
}
