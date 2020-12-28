using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    void Start()
    {
        Transform floor = transform.GetChild(0);
        foreach (Transform child in floor)
        {
            GameManager.instance.floorTiles.Add(child);
        }

        Transform spawn = transform.GetChild(1);
        GameManager.instance.player.transform.position = new Vector3(spawn.position.x,1,spawn.position.z);
        GameManager.instance.map = this;
    }

    public Vector3 GetSpawn()
    {
        return transform.GetChild(1).position;
    }
}
