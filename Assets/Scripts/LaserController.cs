using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform startPoint;
    public HookController hookController;

    public GrappleController grappleController;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grappleController.inHand && !hookController.attached)
        {
            lineRenderer.enabled = true;
            RaycastHit hit;
            Physics.Raycast(startPoint.position, startPoint.forward, out hit);
            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
            lineRenderer.enabled = false;
    }
}
