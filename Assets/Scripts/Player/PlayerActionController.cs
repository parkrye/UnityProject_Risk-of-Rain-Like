using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    public Hero hero;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
