using System.Collections;
using System.Collections.Generic;
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

    bool gameOver = false;
        public bool GameOver { get { return gameOver; } }
    void Awake()
    {
        Instance = this;
        player = GameObject.FindObjectOfType<Player>();
        enemy = GameObject.FindObjectOfType<Enemy>();
    }

    void OnEnable()
    {
        Player.OnPlayerDied += OnPlayerDied;
        Enemy.OnEnemyDied += OnEnemyDied;

    }

    void OnDisable()
    {
        Player.OnPlayerDied -= OnPlayerDied;
        Enemy.OnEnemyDied -= OnEnemyDied;
    }


    void OnEnemyDied()
    {
        
       
    }

    public void AddRespawnObj(int npc_id, Vector3 spwanPos, GameObject go)
    {
        respawnEnemies.Add(new RespawnEnemy(npc_id, spwanPos, go));
    }


    public void RespawnNpc()
    {
        foreach (RespawnEnemy enemy in respawnEnemies)
        {
            enemy.enemy.SetActive(true);
        }
    }
    void OnPlayerDied()
    {
        player.transform.position = Player.spawnLocation;
        player.SetHealth(3);
        RespawnNpc();
        
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
