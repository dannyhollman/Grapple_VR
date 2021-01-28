using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawns;

    public Transform GetSpawnPosition()
    {
        return spawns[Random.Range(0, 4)];
    }
}
