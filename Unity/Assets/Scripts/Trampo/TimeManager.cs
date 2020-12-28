using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Text timeText;
    private float elapsedTime = 0;
    [SerializeField] private GameObject popfinDefaite;
    [SerializeField] private GameObject popfinVictoire;
    [SerializeField] private Text timeoutText;
    [SerializeField] private Text wavesText;
    [SerializeField] private Text victoryText;
    // Start is called before the first frame update
    void Start()
    {
        popfinVictoire.SetActive(false);
        popfinDefaite.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.victory)
        {
            Time.timeScale = 0f;
            victoryText.text = "Vous avez battu " + GameManager.numberOfWaves.ToString() + " vagues et le Boss en " + timeText.text;
            popfinVictoire.SetActive(true);
        }
        else if (GameManager.defeat)
        {
            Time.timeScale = 0f;
            timeoutText.text = "Votre temps : " + timeText.text;
            wavesText.text = "Nombre de vagues battues : " + GameManager.currentWave.ToString();
            popfinDefaite.SetActive(true);
        }
        else
        {
            elapsedTime += Time.deltaTime;
            timeText.text = elapsedTime.ToString("f2") + " s";
        }
    }
        
        
  


    
}