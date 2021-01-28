using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Bolt;

public class NewMovementController : MonoBehaviour
{
    public float speed = 1;
    public XRNode leftHand;
    public XRNode rightHand;
    public float gravity = -9.81f;
    public LayerMask groundLayer;

    private bool sprinting;
    private float fallingSpeed;
    private XRRig rig;
    private Vector2 inputAxis;
    private bool inputClick;
    private bool inputRightSecondaryButton;
    private bool inputLeftSecondaryButton;
    private CharacterController character;

    public static bool grappleMode;

    public AudioSource audioSource;
    public AudioClip footstep;

    private float lastFootstep;
    private float lastPress;

    public GameObject menuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = FindObjectOfType<XRRig>();
        lastFootstep = Time.time;
        lastPress = Time.time;
        grappleMode = false;

        transform.position = FindObjectOfType<SpawnManager>().GetSpawnPosition().position;
    }


    // Update is called once per frame
    void Update()
    {
        InputDevice leftDevice = InputDevices.GetDeviceAtXRNode(leftHand);
        InputDevice rightDevice = InputDevices.GetDeviceAtXRNode(rightHand);
        leftDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
        leftDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out inputClick);
        leftDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out inputLeftSecondaryButton);
        rightDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out inputRightSecondaryButton);

        // Sprint
        if (inputClick && !sprinting)
        {
            sprinting = true;
            speed *= 2;
        }
        else if (inputAxis == Vector2.zero && sprinting)
        {
            sprinting = false;
            speed /= 2;
        }
        // Switch gun mode
        if (inputRightSecondaryButton && Time.time - lastPress > 0.5f)
        {
            grappleMode = !grappleMode;
            lastPress = Time.time;
        }
        // View scoreboard
        if (inputLeftSecondaryButton)
        {
            menuCanvas.SetActive(true);
        }
        else
        {
            menuCanvas.SetActive(false);
        }
        // Play footstep audio
        if ((character.velocity.x != 0 || character.velocity.z != 0) && CheckIfGrounded())
        {
            if (!audioSource.isPlaying && Time.time - lastFootstep > 0.5f)
            {
                audioSource.clip = footstep;
                audioSource.Play();
                lastFootstep = Time.time;
            }

        }
        else
        {
            audioSource.Stop();
        }
    }


    private void FixedUpdate()
    {
        ColliderFollowHeadset();

        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        bool isGrounded = CheckIfGrounded();
        if (isGrounded)
            fallingSpeed = 0;
        else
            fallingSpeed += gravity * Time.fixedDeltaTime;
        character.Move(((direction * speed) + (Vector3.up * fallingSpeed)) * Time.fixedDeltaTime);
    }


    void ColliderFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2, capsuleCenter.z);
    }

    bool CheckIfGrounded()
    {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hit, rayLength, groundLayer);
        return hasHit;
    }
}
