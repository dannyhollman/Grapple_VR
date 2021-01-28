﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class VRAnimatorController : EntityEventListener<IVRPlayerState>
{
    public float speedThreshold = 0.1f;
    public float smoothing = 1;
    private Animator animator;
    private Vector3 previousPos;
    private VRRig vrRig;

    // Start is called before the first frame update
    //void Start()
    public override void Attached()
    {
        if (entity.IsOwner)
        {
            animator = GetComponent<Animator>();
            vrRig = GetComponent<VRRig>();
            previousPos = vrRig.head.vrTarget.position;
        }
    }

    // Update is called once per frame
    //void Update()
    public override void SimulateOwner()
    {
        if (vrRig.head.vrTarget)
        {
            Vector3 headsetSpeed = (vrRig.head.vrTarget.position - previousPos) / Time.deltaTime;
            headsetSpeed.y = 0;

            Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpeed);
            previousPos = vrRig.head.vrTarget.position;

            float previousDirectionX = animator.GetFloat("DirectionX");
            float previousDirectionY = animator.GetFloat("DirectionY");

            animator.SetBool("isMoving", headsetLocalSpeed.magnitude > speedThreshold);
            animator.SetFloat("DirectionX", Mathf.Lerp(previousDirectionX, Mathf.Clamp(headsetLocalSpeed.x, -1, 1), smoothing));
            animator.SetFloat("DirectionY", Mathf.Lerp(previousDirectionY, Mathf.Clamp(headsetLocalSpeed.z, -1, 1), smoothing));
        }
    }
}
