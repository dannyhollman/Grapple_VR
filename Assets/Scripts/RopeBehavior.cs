using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class RopeBehavior : EntityBehaviour<IGrappleGunState>
{
    public override void Attached()
    {
        state.SetTransforms(state.Rope, transform);
    }
}
