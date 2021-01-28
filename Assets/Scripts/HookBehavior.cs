using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class HookBehavior : EntityBehaviour<IGrappleGunState>
{
    public override void Attached()
    {
        state.SetTransforms(state.Hook, transform);
    }
}
