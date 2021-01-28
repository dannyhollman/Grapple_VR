using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteractable : XRBaseInteractable
{
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);

        if (interactor is XRDirectInteractor)
        {
            Debug.Log("Climb detected");
            ClimbController.climbingHand = interactor.GetComponent<XRController>();
        }
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);

        if (interactor is XRDirectInteractor)
        {
            if (ClimbController.climbingHand && ClimbController.climbingHand.name == interactor.name)
                ClimbController.climbingHand = null;
        }
    }
}
