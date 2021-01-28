using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Bolt;

//public class GrappleController : MonoBehaviour
//public class GrappleController : EntityEventListener<IGrappleGunState>
public class GrappleController : EntityBehaviour<IGrappleGunState>
{
    public GameObject hook;
    public GameObject rope;
    public Transform firePoint;
    public NewMovementController movementController;
    public AudioSource audioSource;
    public AudioClip grappleShoot;
    public AudioClip gunshot;

    private bool grappling;

    public bool inHand;
    public bool inHolster;

    public GrappleEventListener eventListener;

    //private void Start()
    public override void Attached()
    {
        if (entity.IsOwner)
            movementController = FindObjectOfType<NewMovementController>();
    }

    //private void Update()
    public override void SimulateOwner()
    {
        if (NewMovementController.grappleMode)
        {
            hook.SetActive(true);
            rope.SetActive(true);
        }
        else
        {
            hook.SetActive(false);
            rope.SetActive(false);
        }
        if (grappling)
        {
            rope.SetActive(true);
        }
        else
        {
            //rope.SetActive(false);
        }
    }

    // Fire button pressed
    public void OnTriggerPress()
    {
        if (NewMovementController.grappleMode)
        {
            if (entity.IsOwner)
            {
                grappling = true;
                //hook.GetComponent<HookController>().Shoot();
                hook.GetComponent<HookController>().shoot = true;
                movementController.enabled = false;
            }

            audioSource.clip = grappleShoot;
            audioSource.Play();
        }

        else
        {
            if (entity.IsOwner)
            {
                GetComponentInChildren<ParticleSystem>().Play();
                Shoot();
            }
            audioSource.Stop();
            audioSource.clip = gunshot;
            audioSource.Play();
        }
    }

    //  Fire button released
    public void OnTriggerRelease()
    {
        if (NewMovementController.grappleMode)
        {
            grappling = false;
            hook.GetComponent<HookController>().Retract();
            movementController.enabled = true;
        }

        else
        {
            GetComponentInChildren<ParticleSystem>().Stop();
        }
    }

    public void Shoot()
    {
        if (entity.IsOwner)
        {
            Debug.Log("Shooting on network");
            using (var hits = BoltNetwork.RaycastAll(new Ray(firePoint.position, firePoint.forward)))
            {
                for (int i = 0; i < hits.count; ++i)
                {
                    var hit = hits.GetHit(i);
                    var receiver = hit.body.GetComponent<BoltEntity>();

                    if ((receiver != null))
                    {
                        SendDamageEvent(10, state.Owner, receiver);
                    }
                }
            }
        }
    }

    public void SendDamageEvent(int damage, BoltEntity sender, BoltEntity receiver)
    {
        Debug.Log("Sending damage evemt");
        var damageEvent = ApplyDamageEvent.Create(Bolt.ReliabilityModes.ReliableOrdered);
        damageEvent.Damage = damage;
        damageEvent.Sender = sender;
        damageEvent.Receiver = receiver;
        damageEvent.Send();
    }

    public void InHand()
    {
        inHand = true;
    }

    public void NotInHand()
    {
        inHand = false;
    }

    public void InHolster()
    {
        inHolster = true;
    }

    public void NotInHolster()
    {
        inHolster = false;
    }
}