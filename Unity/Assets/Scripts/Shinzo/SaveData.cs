using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int currentWaveData;
    public int playerType;

    public SaveData ()
    {
        currentWaveData = GameManager.currentWave;
        playerType = GameManager.selectedCharacter;
    }
}
