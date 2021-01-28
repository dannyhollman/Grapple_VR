using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class VRFootIK : EntityEventListener<IVRPlayerState>
{
    public Vector3 footOffset;
    private Animator animator;
    [Range(0,1)]
    public float rightFootPosWeight = 1;
    [Range(0,1)]
    public float leftFootPosWeight = 1;
    [Range(0, 1)]
    public float rightFootRotWeight = 1;
    [Range(0, 1)]
    public float leftFootRotWeight = 1;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        Vector3 rightFootPos = animator.GetIKPosition(AvatarIKGoal.RightFoot);
        RaycastHit hit;

        bool hasHit = Physics.Raycast(rightFootPos + Vector3.up, Vector3.down, out hit);
        if (hasHit && hit.transform.gameObject.layer != 11)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootPosWeight);
            animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + footOffset);

            Quaternion rightFootRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightFootRotWeight);
            animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRotation);

        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
        }

        Vector3 leftFootPos = animator.GetIKPosition(AvatarIKGoal.LeftFoot);

        hasHit = Physics.Raycast(leftFootPos + Vector3.up, Vector3.down, out hit);
        if (hasHit && hit.transform.gameObject.layer != 11)
        {

            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootPosWeight);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + footOffset);

            Quaternion leftFootRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftFootRotWeight);
            animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootRotation);

        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
        }
    }
}
