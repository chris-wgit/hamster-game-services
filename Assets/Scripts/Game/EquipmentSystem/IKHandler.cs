using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandler : MonoBehaviour
{
    private Animator animator;

    public Transform leftHandler;
    public Transform rightHandler;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnSetHandler()
    {
        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandler.position);
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandler.position);
    }
}
