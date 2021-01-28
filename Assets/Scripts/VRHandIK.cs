using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class VRHandIK : EntityBehaviour<IVRPlayerState>
{
    public Vector3 rightHandPositionOffset;
    public Vector3 rightHandRotationOffset;
    public Vector3 leftHandPositionOffset;
    public Vector3 leftHandRotationOffset;

    Animator animator;
    public override void Attached()
    {
        animator = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        if (entity.IsOwner)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);

            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);

            animator.SetLookAtWeight(1);

            animator.SetIKPosition(AvatarIKGoal.LeftHand, StaticData.rigManager.leftHand.TransformPoint(leftHandPositionOffset));
            animator.SetIKPosition(AvatarIKGoal.RightHand, StaticData.rigManager.rightHand.TransformPoint(rightHandPositionOffset));

            animator.SetIKRotation(AvatarIKGoal.LeftHand, StaticData.rigManager.leftHand.rotation * Quaternion.Euler(leftHandRotationOffset));
            animator.SetIKRotation(AvatarIKGoal.RightHand, StaticData.rigManager.rightHand.rotation * Quaternion.Euler(rightHandRotationOffset));


            animator.SetLookAtPosition(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 2.0f)));
        }

        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);

            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);

            animator.SetLookAtWeight(1);

            animator.SetIKPosition(AvatarIKGoal.LeftHand, state.LeftHand.Transform.position + leftHandPositionOffset);
            animator.SetIKPosition(AvatarIKGoal.RightHand, state.RightHand.Transform.position + rightHandPositionOffset);

            animator.SetIKRotation(AvatarIKGoal.LeftHand, state.LeftHand.Transform.rotation * Quaternion.Euler(leftHandRotationOffset));
            animator.SetIKRotation(AvatarIKGoal.RightHand, state.RightHand.Transform.rotation * Quaternion.Euler(rightHandRotationOffset));

        }
    }
}
