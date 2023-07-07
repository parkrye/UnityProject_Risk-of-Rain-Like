using UnityEngine;

public class SelectSceneAnimationPlayer : MonoBehaviour
{
    [SerializeField] Animator[] animator = new Animator[3];

    public void PlayAnimation(int num)
    {
        animator[num].SetTrigger("Start");
    }
}
