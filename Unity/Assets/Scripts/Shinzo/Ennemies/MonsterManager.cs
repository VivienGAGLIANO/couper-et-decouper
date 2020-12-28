using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager instance;

    private Player player;

    public NiceLight lighto;

    public enum MonsterType {Archer, Soldier, Demon, Boss};
    [SerializeField] private Monster originalArcher;
    [SerializeField] private Monster originalSoldier;
    [SerializeField] private Monster originalDemon;
    [SerializeField] private Monster originalBoss;

    private static Queue<Monster> deadArchers = new Queue<Monster>();
    private static Queue<Monster> deadSoldiers = new Queue<Monster>();
    private static Queue<Monster> deadDemons = new Queue<Monster>();
    private static Queue<Monster> deadBosses = new Queue<Monster>();

    [SerializeField] private float archerSpawnRate = 0.4f;
    [SerializeField] private float soldierSpawnRate = 0.4f;
    [SerializeField] private float demonSpawnRate = 0.2f;


    private float monsterFrequencyTimer = 1f;
    private float nextWaveTimer = 5f;
    private int lastWaveEnnemies = 0;
    public int currentWaveEnnemies = 1;
    public static int deadMonstersCount = 0;

    public AudioClip spawnSound;

    public AudioClip VictoryMusic;

    private void Awake()
    {
        if(instance)
        {
            Debug.LogError("Il y a déjà une instance de MonsterManager " + name);
            Destroy(this);
        }
        else
        {
            instance = this;
            player = GameManager.instance.player;
            int tmp = 0;
            for (int i = 0; i<GameManager.currentWave; i++)
            {
                tmp = lastWaveEnnemies;
                lastWaveEnnemies = currentWaveEnnemies;
                currentWaveEnnemies += tmp;
            }
        }
    }

    void Start()
    {
        StartCoroutine(StartWave());
    }

    void Update()
    {
        if (deadMonstersCount == currentWaveEnnemies)
        {
            deadMonstersCount = 0;
            
            var tmp = lastWaveEnnemies;
            lastWaveEnnemies = currentWaveEnnemies;
            currentWaveEnnemies += tmp;
            
            GameManager.currentWave++ ;
            if (GameManager.currentWave == 10)
            {
                Instantiate(originalBoss, 2 * new Vector3(0, 2, 2), Quaternion.identity);
            }
            if (GameManager.currentWave>GameManager.numberOfWaves)
            {
                StartCoroutine(VictoryRoutine());
            }
            else
            {
                GameManager.instance.player.ResetForNewWave();
                StartCoroutine(StartWave());
                Debug.Log("New Wave");
            }
        }
    }

    private IEnumerator StartWave()
    {
        lighto.SetLight(GameManager.currentWave);
        yield return new WaitForSeconds(nextWaveTimer);

        for (var i = 0 ; i < currentWaveEnnemies ; i++)
        {
            SpawnMonster();
            yield return new WaitForSeconds(monsterFrequencyTimer);
        }
    }

    private void SpawnMonster()
    {
        var threshold1 = archerSpawnRate;
        var threshold2 = threshold1 + soldierSpawnRate;

        var monsterChosed = Random.Range(0f,1f);

        GameObject monster;

        if (monsterChosed < threshold1) 
        {
            monster = GetMonster(MonsterType.Archer);
            monster.GetComponent<Archer>().Reset();
        }
        else if (threshold1 <= monsterChosed && monsterChosed < threshold2)
        {
            monster = GetMonster(MonsterType.Soldier);
            monster.GetComponent<Soldier>().Reset();
        }
        else
        {
            monster = GetMonster(MonsterType.Demon);
        }

        SoundManager.instance.PlaySFX(spawnSound,0.2f);
        var spawnPoint = Random.Range(1,SpawnPoint.spawnInstances.Count);
        var spawnPos = SpawnPoint.spawnInstances[spawnPoint-1].transform.position;
        monster.transform.position = new Vector3(spawnPos.x, monster.transform.position.y, spawnPos.z);
        monster.transform.rotation = Quaternion.Euler(0, Random.Range(0f,360f), 0);
        
    }

    public GameObject GetMonster(MonsterType monsterType){

		Monster monster;

        if (monsterType == MonsterType.Archer)
        {
            if (deadArchers.Count > 0) {
			monster = deadArchers.Dequeue();
			monster.gameObject.SetActive(true);
            }
            else {
                monster = Instantiate(originalArcher);
            }
        }

        else if (monsterType == MonsterType.Soldier)
        {
            if (deadSoldiers.Count > 0) {
			monster = deadSoldiers.Dequeue();
			monster.gameObject.SetActive(true);
            }
            else {
                monster = Instantiate(originalSoldier);
            }
        }

        else
        {
            if (deadDemons.Count > 0) {
			monster = deadDemons.Dequeue();
			monster.gameObject.SetActive(true);
            }
            else {
                monster = Instantiate(originalDemon);
            }
        }

        if (monster.rb == null)
        {
            monster.rb = monster.GetComponent<Rigidbody>();
        }
        return monster.gameObject;

    }

    public static void EndMonster(Monster monster) {

        deadMonstersCount++;
		monster.gameObject.SetActive(false);

        if (monster is Archer)
        {
            deadArchers.Enqueue(monster);
        }
        else if (monster is Soldier)
        {
            deadSoldiers.Enqueue(monster);
        }
		else
        {
            deadDemons.Enqueue(monster);
        }
	}

    private IEnumerator VictoryRoutine()
    {
        SoundManager.instance.PlayMusicWithCrossFade(VictoryMusic);
        SoundManager.instance.ChangeLoop(false);
        yield return new WaitForSeconds(3f);
        GameManager.victory = true;
    }

}
