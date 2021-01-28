using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

[BoltGlobalBehaviour("Demo")]
public class NetworkCallbacks : GlobalEventListener
{
    public override void SceneLoadLocalDone(string scene)
    {
        if (!BoltNetwork.IsServer)
        {
            BoltEntity player = BoltNetwork.Instantiate(BoltPrefabs.Character_Model, new Vector3(0, 2, 0), Quaternion.identity);
        }
    }

    public override void OnEvent(ApplyDamageEvent evnt)
    {
        Debug.Log("Event triggered in network callbacks");
        if (evnt.Receiver && evnt.Sender)
            evnt.Receiver.GetComponent<PlayerBehavior>().ApplyDamage(evnt.Damage, evnt.Sender);
        Debug.Log(evnt.Sender);
    }

    public override void OnEvent(KillEvent evnt)
    {
        Debug.Log("Kill event triggered");
        if (evnt.Entity)
            evnt.Entity.GetState<IVRPlayerState>().Kills += 1;
    }
}
