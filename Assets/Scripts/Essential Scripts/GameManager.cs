using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOver;

    bool gameOver = false;
        public bool GameOver { get { return gameOver; } }
    void Awake()
    {
        Instance = this;
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
        gameOver = true;
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
