using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    void Start()
    {
        GameManager.selectedCharacter = 0;
    }

    public void CharacterSelection(int select)
    {
        GameManager.instance.CharacterSelection(select);
    }
    public void StartGame()
    {
        GameManager.instance.StartGame();
    }
    public void LoadGame()
    {
        GameManager.instance.LoadGame();
    }
    public void QuitGame()
    {
        GameManager.instance.QuitGame();
    }
}
