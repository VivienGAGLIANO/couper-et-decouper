using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTiling : MonoBehaviour
{
    public float offsetBase = 1f;
    public float change = 0f;
    void Start()
    {
        MeshRenderer rend = GetComponent<MeshRenderer> ();
        float offsetx = offsetBase * (transform.position.x % 16)/4;
        float offsety = offsetBase * (transform.position.y % 16)/4;

        //GetComponent<MeshRenderer> ().material.SetTextureOffset("_MainTex", new Vector2(7f, 0f));
        

    }   
    void Update()
    {
        //Debug.Log("1");
        GetComponent<MeshRenderer> ().material.SetTextureOffset("_BaseMap", new Vector2(change, 0f));
        Debug.Log(GetComponent<MeshRenderer>().material.GetTextureOffset("_BaseMap")         );
        // Debug.Log("2");
        // Debug.Log(GetComponent<MeshRenderer>().sharedMaterial.GetTextureOffset("_MainTex")         );
        // Debug.Log("3");
        // Debug.Log(GetComponent<MeshRenderer>().material.GetTextureOffset("_MainTex")         );
        // Debug.Log("4");
        // Debug.Log(GetComponent<MeshRenderer>().sharedMaterial.GetTextureOffset("_MainTex")         );
        // Debug.Log("5");
        // Debug.Log( GetComponent<MeshRenderer>().materials[0].GetTextureOffset("_MainTex")  );
        
        
    }
}

