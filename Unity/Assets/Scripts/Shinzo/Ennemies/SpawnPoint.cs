using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    
    public static List<SpawnPoint> spawnInstances = new List<SpawnPoint>();

    void Start()
    {
        spawnInstances.Add(this);
    }

}
