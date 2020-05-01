using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOver;
    public static event GameDelegate OnEnemyDeath;

    public static List<RespawnEnemy> respawnEnemies = new List<RespawnEnemy>();

    Player player;
    Enemy enemy;
    ZiplineHandler ziplineHandler;

    bool gameOver = false;
        public bool GameOver { get { return gameOver; } }
    void Awake()
    {
        Instance = this;
        player = GameObject.FindObjectOfType<Player>();
        enemy = GameObject.FindObjectOfType<Enemy>();
        ziplineHandler = GameObject.FindObjectOfType<ZiplineHandler>();
    }

    void OnEnable()
    {
        Player.OnPlayerDied += OnPlayerLostLife;
        Enemy.OnEnemyDied += OnEnemyDied;

    }

    void OnDisable()
    {
        Player.OnPlayerDied -= OnPlayerLostLife;
        Enemy.OnEnemyDied -= OnEnemyDied;
    }


    void OnEnemyDied()
    {
        
       
    }

    public void AddRespawnObj(int npc_id, Vector3 spwanPos, GameObject go, int MAX_HEALTH)
    {
        respawnEnemies.Add(new RespawnEnemy(npc_id, spwanPos, go, MAX_HEALTH));
    }


    public void RespawnNpc()
    {
        foreach (RespawnEnemy enemy in respawnEnemies)
        {
            Vector3 spawnLocation = enemy.spawnPoint;
            spawnLocation.x = enemy.spawnPoint.x -= .3f;
            enemy.enemy.transform.position = spawnLocation;
            enemy.enemy.SetActive(true);
            enemy.enemy.GetComponent<Enemy>().SetState(Enemy.State.Idle);
            enemy.enemy.GetComponent<Enemy>().UpdateHealth(enemy.MAX_HEALTH);
        }
        respawnEnemies.Clear();
    }
    void OnPlayerLostLife()
    {
        if(player.GetHealth() <= 0)
        {
            OnPlayerDied();
        }
    }
    IEnumerator UpdateHealth()
    {
        yield return new WaitForSeconds(0.8f);
        player.UpdateHealth(3);
        RespawnNpc();
    }

    void OnPlayerDied()
    {
        player.transform.position = Player.spawnLocation;
        StartCoroutine(UpdateHealth());
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        ziplineHandler.ResetZipline();
        player.ridingZipline = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        OnGameStarted(); //event
    }

    public void ConfirmGameOver()
    {
        OnGameOver(); // event
    }
}
