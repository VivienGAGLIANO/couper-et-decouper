using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VagueUI : MonoBehaviour
{
    [SerializeField] private Text vagueText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vagueText.text = "Vague n° : " + (GameManager.currentWave + 1).ToString();
    }
}
