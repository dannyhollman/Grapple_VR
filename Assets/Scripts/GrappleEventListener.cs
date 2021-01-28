using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

//public class GrappleEventListener : EntityEventListener<IGrappleGunState>
public class GrappleEventListener : GlobalEventListener
{
    /*
    public void SendDamageEvent(int damage, BoltEntity entity)
    {
        var damageEvent = ApplyDamageEvent.Create(Bolt.ReliabilityModes.ReliableOrdered);
        damageEvent.Damage = damage;
        damageEvent.Entity = entity;
        damageEvent.Send();
    }
    */
}
