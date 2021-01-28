using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class ServerCheck : MonoBehaviour
{
    public GameObject serverCamera;
    // Start is called before the first frame update
    void Start()
    {
        if (BoltNetwork.IsServer)
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
            serverCamera.SetActive(true);
        }
    }
}
