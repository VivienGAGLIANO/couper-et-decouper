using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInit : MonoBehaviour
{
    void Start()
    {
        GameObject map = GameObject.Find("MAP1");
        Transform floor = map.transform.GetChild(0);
        foreach (Transform child in floor)
        {
            GameManager.instance.floorTiles.Add(child);
        }
    }
}
