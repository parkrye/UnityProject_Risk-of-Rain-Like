using System.Collections;
using UnityEngine;

public class PlayerGroundChecker : MonoBehaviour
{
    public bool IsGround
    {
        get 
        { 
            if (ready && groundCounter > 0) 
                return true;
            return false;
        }
    }
    int groundCounter;
    bool ready;

    void Awake()
    {
        groundCounter = 0;
        ready = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer) == LayerMask.GetMask("Ground"))
        {
            groundCounter++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if((1 << other.gameObject.layer) == LayerMask.GetMask("Ground"))
        {
            groundCounter--;
        }
    }

    public void JumpReady()
    {
        StartCoroutine(JumpReadyRoutine());
    }

    IEnumerator JumpReadyRoutine()
    {
        ready = false;
        yield return new WaitForSeconds(0.5f);
        ready = true;
    }
}
