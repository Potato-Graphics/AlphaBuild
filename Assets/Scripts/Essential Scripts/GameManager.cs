using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOver;

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
    }

    void OnDisable()
    {
        Player.OnPlayerDied -= OnPlayerDied;
    }

    void OnPlayerDied()
    {
        player.transform.position = Player.spawnLocation;
        player.SetHealth(3);
        foreach (RespawnEnemy enemy in respawnEnemies)
        {
            enemy.enemy.SetActive(true);
        }
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
