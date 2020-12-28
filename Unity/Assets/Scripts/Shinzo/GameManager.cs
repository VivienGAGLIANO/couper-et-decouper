using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public static bool gameIsPaused = false;
    public static bool victory = false;
    public static bool defeat = false;

    public KeyCode action_1 { get; set; }
    public KeyCode action_2 { get; set; }
    public KeyCode action_3 { get; set; }

    //Selection personnage
    public static int selectedCharacter = 0;
    [SerializeField] private GameObject popselectionCharacter;

    public Player player;
    [SerializeField] private Player[] characters = new Player[3];

    public Map map;

    //Gestion des vagues
    public static int numberOfWaves = 10;
    public static int currentWave = 0;

    public AudioClip mainMusic;

    public AudioClip[] looseMusics = new AudioClip[12];


    public List<Transform> floorTiles = new List<Transform>();

    public void Awake()
    {
        if(instance)
        {
            Debug.Log("Il y a déjà une instance de GameManager " + name);
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        action_1 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("1Key", "Mouse0"));
        action_2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("2Key", "Mouse1"));
        action_3 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("3Key", "Space"));


    }


    void Start()
    {
        DontDestroyOnLoad(gameObject);

        //Stockage des positions
        

    }

    void Update()
    {

    }

    public void CharacterSelection(int select)
    {
        selectedCharacter = select;
    }



    public void QuitGame()
    {
        Debug.Log("Quit!!!");
        Application.Quit();
    }

    public void StartGame()
    {
        if (selectedCharacter == 0)
        {
            popselectionCharacter.SetActive(true);
        }
        else
        {
            //Gérer ici le lancement des différentes maps
            SceneManager.LoadScene(Random.Range(1, 4));

            player = Instantiate(characters[selectedCharacter -1],new Vector3(0,1,0),Quaternion.identity);
            DontDestroyOnLoad(player);
            SoundManager.instance.PlayMusicWithCrossFade(mainMusic);
        }
    }

    public void LoadGame()
    {
        SaveData data = SaveSystem.LoadGame();

        currentWave = data.currentWaveData;
        selectedCharacter = data.playerType;
        StartGame();

    }

    public void UpdateAbilityKeys()
    {
        player.abilitiesKeyboardMapping = new KeyCode[] { action_1, action_2, action_3 };
    }


    public void LoadMenu()
    {
        Destroy(player.gameObject);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        gameIsPaused = false;
        victory = false;
        defeat = false;
    }
}
