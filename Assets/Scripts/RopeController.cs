using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

//public class RopeController : MonoBehaviour
public class RopeController : EntityEventListener<IGrappleGunState>
{
    public Transform startPoint;
    public Transform endPoint;

    //public override void SimulateOwner()
    private void Update()
    {
        transform.position = (startPoint.position + endPoint.position) / 2;
        transform.localScale = new Vector3(transform.localScale.x, Vector3.Distance(endPoint.position, startPoint.position) / 2, transform.localScale.z);
        transform.rotation = Quaternion.LookRotation(endPoint.position - startPoint.position) * Quaternion.Euler(90, 0, 0);
    }
}
