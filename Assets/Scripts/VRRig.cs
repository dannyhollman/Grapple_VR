using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;
    
    public void Map()
    {
        if (vrTarget)
        {
            rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
            rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
        }
    }
}

public class VRRig : EntityEventListener<IVRPlayerState>
{
    public VRMap head;
    public VRMap right;
    public VRMap left;
    public float turnSmoothness = 5;


    public Transform headConstraint;
    public Vector3 headBodyOffset;

    public GameObject bodyRoot;

    public GameObject[] characters;


    // Start is called before the first frame update
    //void Start()
    public override void Attached()
    {
        if (entity.IsOwner)
        {
            head.vrTarget = StaticData.rigManager.head;
            right.vrTarget = StaticData.rigManager.rightHand;
            left.vrTarget = StaticData.rigManager.leftHand;
            //headBodyOffset = transform.position - headConstraint.position;

            //characters[PlayerPrefs.GetInt("charID")].SetActive(true);

        }
        //characters[state.ModelID].SetActive(true);

        headBodyOffset = transform.position - headConstraint.position;

    }



    // Update is called once per frame
    void Update()
    {
        transform.position = headConstraint.position + headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized, turnSmoothness * Time.deltaTime);
        //transform.forward = Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized;

        //if (Quaternion.Angle(Quaternion.Euler(0, bodyRoot.transform.rotation.eulerAngles.y, 0), Quaternion.Euler(0, head.vrTarget.transform.rotation.eulerAngles.y, 0)) > 90)
        //StartCoroutine(LerpBody());

        if (entity.IsOwner)
        {
            head.Map();
            right.Map();
            left.Map();
        }
    }

    IEnumerator LerpBody()
    {
        float startTime = Time.time;
        while (Time.time < startTime + 0.3f)
        {
            transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized, turnSmoothness * Time.deltaTime);
            yield return null;
        }
    }
}
