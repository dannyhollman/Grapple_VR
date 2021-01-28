using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using UnityEngine.XR.Interaction.Toolkit;

public class test : EntityEventListener<ITestState>
{
    public override void Attached()
    {
        state.SetTransforms(state.NewProperty, transform);

        if (!entity.IsOwner)
        {
            GetComponent<XRGrabInteractable>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
