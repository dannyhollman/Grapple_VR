using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbController : MonoBehaviour
{
    private CharacterController character;
    public static XRController climbingHand;
    public NewMovementController movementController;

    public Vector3 prevPos;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        //prevPos = climbingHand.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (climbingHand)
        {
            movementController.enabled = false;
            Climb(prevPos);
            //prevPos = climbingHand.transform.position;

        }
        else
            movementController.enabled = true;

            }

    void Climb(Vector3 prevPos)
    {
        InputDevices.GetDeviceAtXRNode(climbingHand.controllerNode).TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);

        //character.Move(transform.rotation * -velocity * Time.fixedDeltaTime);
    }
}
