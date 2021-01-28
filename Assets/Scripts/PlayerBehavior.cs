using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerBehavior : Bolt.EntityBehaviour<IVRPlayerState>
{
    public Transform headConstraint;
    public Transform rightHand;
    public Transform leftHand;

    public Transform dropZone;

    public XRSocketInteractor socket;


    public override void Attached()
    {
        state.SetTransforms(state.PlayerTransform, transform);
        state.SetTransforms(state.Head, headConstraint);
        state.SetTransforms(state.RightHand, rightHand);
        state.SetTransforms(state.LeftHand, leftHand);
        state.SetAnimator(GetComponent<Animator>());

        state.Animator.SetLayerWeight(0, 1);

        state.Health = 100;
        state.Kills = 0;
        state.Deaths = 0;
        state.AddCallback("Health", HealthCallback);
        state.AddCallback("Kills", KillCallback);
        state.AddCallback("ModelID", ModelIDCallback);

        if (entity.IsOwner)
        {
            BoltEntity gun = BoltNetwork.Instantiate(BoltPrefabs.test_pistol, dropZone.position, Quaternion.identity);
            gun.GetState<IGrappleGunState>().Owner = entity;
            state.ModelID = PlayerPrefs.GetInt("charID");
            state.Username = PlayerPrefs.GetString("Username");
        }
    }

    private void ModelIDCallback()
    {
        GetComponent<VRRig>().characters[state.ModelID].SetActive(true);
    }

    private void KillCallback()
    {
        Debug.Log("Kills: " + state.Kills);
    }

    private void HealthCallback()
    {
        Debug.Log("Current health: " + state.Health);
        if (state.Health <= 0)
        {
            Respawn();
            //BoltNetwork.Destroy(gameObject);
        }
    }

    public void ApplyDamage(int damage, BoltEntity sender)
    {
        state.Health -= damage;
        if (state.Health <= 0)
        {
            //sender.GetState<IVRPlayerState>().Kills += 1;
            SendKillEvent(sender);
        }
    }

    public void SendKillEvent(BoltEntity receiver)
    {
        Debug.Log("Sending kill event");
        var killEvent = KillEvent.Create(Bolt.ReliabilityModes.ReliableOrdered);
        killEvent.Entity = receiver;
        killEvent.Send();
    }


    public void Respawn()
    {
        if (entity.IsOwner)
        {
            state.Health = 100;
            StaticData.rigManager.vrPlayerController.GetComponent<CharacterController>().enabled = false;
            StaticData.rigManager.vrPlayerController.transform.position = FindObjectOfType<SpawnManager>().GetSpawnPosition().position;
            //StaticData.rigManager.vrPlayerController.transform.position = new Vector3(0.2f, 0.9f, 0);
            StaticData.rigManager.vrPlayerController.GetComponent<CharacterController>().enabled = true;
        }
    }
}
