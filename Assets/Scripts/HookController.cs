using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

//public class HookController : MonoBehaviour
public class HookController : EntityEventListener<IGrappleGunState>
{
    public bool attached;
    public bool retracting;
    public Transform retractionPoint;
    public float retractionSpeed;
    public CharacterController character;
    public NewMovementController movementController;

    public bool shoot;

    private RaycastHit hitPoint;

    //private void Awake()
    public override void Attached()
    {
        if (entity.IsOwner)
        {
            retracting = true;
            character = FindObjectOfType<CharacterController>();
            movementController = FindObjectOfType<NewMovementController>();
        }
    }

    private void Update()
    {
        //if (retracting)
        if (entity.IsOwner && retracting)
        {
            //Debug.Log("Retracting");
            transform.position = Vector3.MoveTowards(transform.position, retractionPoint.position, 3f) + retractionPoint.forward * .05f;
            transform.rotation = retractionPoint.rotation;
        }
        if (entity.IsOwner && attached)
        {
            transform.position = hitPoint.point;
        }

    }

    //private void Update()
    //private void FixedUpdate()
    public override void SimulateOwner()
    {
        if (shoot)
        {
            shoot = false;
            retracting = false;
            RaycastHit hit;
            //Debug.DrawRay(transform.position, transform.forward * 100, Color.red, 100f);

            //GetComponent<Rigidbody>().isKinema tic = false;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 500f))
            {
                //transform.position = hit.point;
                hitPoint = hit;
                GetComponent<Rigidbody>().isKinematic = true;
                attached = true;
            }
        }
        if (!shoot && !attached && !retracting)
            retracting = true;
        /*
        if (retracting)
        {
            //Debug.Log("Retracting");
            transform.position = Vector3.MoveTowards(transform.position, retractionPoint.position, 3f) + retractionPoint.forward * .05f;
            transform.rotation = retractionPoint.rotation;
        }
        */
        if (attached)
        {
            movementController.enabled = false;
            Vector3 direction = (transform.position - character.transform.position).normalized;
            character.Move(direction * retractionSpeed *  Time.deltaTime);
            if (Vector3.Distance(character.transform.position, transform.position) < 0.2f)
            {
                attached = false;
                retracting = true;
            }
        }
    }

    /*
    public void Shoot()
    {
        Debug.Log("Shoot");
        retracting = false;
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 100, Color.red, 100f);

        //GetComponent<Rigidbody>().isKinema tic = false;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 200f))
        {
            Debug.Log("Hit point: " + hit.point);
            //transform.position = hit.point;
            hitPoint = hit;
            GetComponent<Rigidbody>().isKinematic = true;
            attached = true;
        }
    }
    */

    public void Retract()
    {
        retracting = true;
        attached = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

}
