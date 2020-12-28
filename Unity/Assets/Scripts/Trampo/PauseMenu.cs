using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    

    public GameObject PauseMenuUI;
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameManager.gameIsPaused = false;
        GameManager.instance.player.lookAtMouse = true;
    }

    public void Pause()
    {

        
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameManager.gameIsPaused = true;
        GameManager.instance.player.lookAtMouse = false;

    }

    public void LoadMenu()
    {
        GameManager.instance.LoadMenu();
    }

    public void SaveGame()
    {
        Debug.Log("Partie Sauvegardée");
        SaveSystem.SaveGame();
    }
}

