using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalRigManager : MonoBehaviour
{
    public Camera mainCamera;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;
    public GameObject vrPlayerController;

    private void Start()
    {
        StaticData.rigManager = this;
    }
}
