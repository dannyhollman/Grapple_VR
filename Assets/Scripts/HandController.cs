using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Transform vrTarget;
    public Vector3 offset;

    private void Update()
    {
        transform.position = vrTarget.position + offset;
    }
}
