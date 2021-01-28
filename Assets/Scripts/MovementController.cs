using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class MovementController : MonoBehaviour
{
    public float speed = 1.0f;
    public List<XRController> controllers;
    public XRRig rig;
    public GameObject head = null;
    public CapsuleCollider character;

    // Update is called once per frame
    void Update()
    {
        foreach (XRController xRController in controllers)
        {
            if (xRController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 positionVector))
            {
                if (positionVector.magnitude > 0.15f)
                {
                    Move(positionVector);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        FollowHeadset();
    }

    void FollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2, capsuleCenter.z);
    }

    private void Move(Vector2 positionVector)
    {
        // Apply the touch position to the head's forward Vector
        Vector3 direction = new Vector3(positionVector.x, 0, positionVector.y);
        Vector3 headRotation = new Vector3(0, head.transform.eulerAngles.y, 0);

        // Rotate the input direction by the horizontal head rotation
        direction = Quaternion.Euler(headRotation) * direction;

        // Apply speed and move
        Vector3 movement = direction * speed;
        transform.position += (Vector3.ProjectOnPlane(Time.deltaTime * movement, Vector3.up));
    }
}
