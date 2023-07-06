using UnityEngine;

public class SelectSceneAnimationController : MonoBehaviour
{
    [SerializeField] Animator[] animator = new Animator[3];

    public void PlayAnimation(int num)
    {
        animator[num].SetTrigger("Start");
    }
}
