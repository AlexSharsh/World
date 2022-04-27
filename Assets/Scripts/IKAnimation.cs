using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKAnimation : MonoBehaviour
{
    [SerializeField] private Transform rightHandObj;
    [SerializeField] private Transform leftHandObj;
    [SerializeField] private Transform lookObj;

    [SerializeField] private Animator animator;

    [SerializeField] private float rightHandWeight;
    [SerializeField] private float leftHandWeight;
    [SerializeField] private float leftFootWeight;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void OnAnimatorIK()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeight);

        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandWeight);

        animator.SetLookAtWeight(1);

        if (rightHandObj)
        {
            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
        }

        if (leftHandObj)
        {
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
        }

        if (lookObj)
        {
            animator.SetLookAtPosition(lookObj.position);
        }
    }
}
