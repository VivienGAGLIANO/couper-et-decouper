using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Saver : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;


    public void SaveGame()
    {
        SaveSystem.SaveGame();
    }

    public void SetMusic()
    {
        SoundManager.instance.SetMusicVolume(musicSlider.value);
    }

    public void SetSFX()
    {
        SoundManager.instance.SetSFXVolume(SFXSlider.value);
    }
}
