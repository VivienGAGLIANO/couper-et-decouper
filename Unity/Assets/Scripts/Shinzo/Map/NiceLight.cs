using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiceLight : MonoBehaviour
{
    public Light mylight;

    public int current = 0;

    void Start(){
        mylight = GetComponent<Light>();
    }
    public void SetLight(int i)
    {
        current = i;
        if (i==0)
        {
            transform.position = new Vector3(-37,12,-10);
            Debug.Log(transform.position);
            transform.rotation = Quaternion.Euler(17f,73f,0f);
            mylight.color = new Color32(255,254,239,255);
            mylight.intensity = 1.5f;
            mylight.shadowStrength = 0.5f;
        }
        if (i==1)
        {
            transform.position = new Vector3(-37f,20f,-10f);
            transform.rotation = Quaternion.Euler(27f,73f,0f);
            mylight.color = new Color32(255,254,239,255);
            mylight.intensity = 1.7f;
            mylight.shadowStrength = 0.5f;
        }
        if (i==2)
        {
            transform.position = new Vector3(-27f,30f,-10f);
            transform.rotation = Quaternion.Euler(40f,73f,0f);
            mylight.color = new Color32(255,254,239,255);
            mylight.intensity = 1.8f;
            mylight.shadowStrength = 0.5f;
        }
        if (i==3)
        {
            transform.position = new Vector3(-13f,35f,-8f);
            transform.rotation = Quaternion.Euler(57f,59f,0f);
            mylight.color = new Color32(255,253,218,255);
            mylight.intensity = 1.9f;
            mylight.shadowStrength = 0.5f;
        }
        if (i==4)
        {
            transform.position = new Vector3(0f,39f,0f);
            transform.rotation = Quaternion.Euler(90f,60f,0f);
            mylight.color = new Color32(255,250,200,255);
            mylight.intensity = 2f;
            mylight.shadowStrength = 0.5f;
        }
        if (i==5)
        {
            transform.position = new Vector3(26f,32f,17f);
            transform.rotation = Quaternion.Euler(133f,60f,0f);
            mylight.color = new Color32(255,247,174,255);
            mylight.intensity = 2f;
            mylight.shadowStrength = 0.5f;
        }
        if (i==6)
        {
            transform.position = new Vector3(34f,26f,21f);
            transform.rotation = Quaternion.Euler(150f,60f,0f);
            mylight.color = new Color32(255,227,174,255);
            mylight.intensity = 1.8f;
            mylight.shadowStrength = 0.5f;
        }
        if (i==7)
        {
            transform.position = new Vector3(40f,18f,22f);
            transform.rotation = Quaternion.Euler(160f,59f,0f);
            mylight.color = new Color32(255,195,174,255);
            mylight.intensity = 1.8f;
            mylight.shadowStrength = 0.3f;
        }
        if (i==8)
        {
            transform.position = new Vector3(0f,51f,-4.5f);
            transform.rotation = Quaternion.Euler(67f,0f,8.7f);
            mylight.color = new Color32(121,123,144,255);
            mylight.intensity = 1.5f;
            mylight.shadowStrength = 0f;
        }
        if (i>=9)
        {
            transform.position = new Vector3(23f,51f,-10f);
            transform.rotation =  Quaternion.Euler(54f,-72f,9f);
            mylight.color = new Color32(73,90,255,255);
            mylight.intensity = 2f;
            mylight.shadowStrength = 0.4f;
        }

    }
}
