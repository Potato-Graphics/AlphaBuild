using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    /*
     * Health
     * */
    [SerializeField] private const int MAX_HEALTH = 25;
    private int currentHealth; // enemys current health

    /*
     * 
     */
    public Transform spawnPoint;
    [SerializeField] private Vector3 playerPosition; // players position
    [SerializeField] private Vector3 enemyPosition; // enemy position
    [SerializeField] public bool collidingWithPlayer = false;

    [SerializeField]private bool movingRight = true;
    


    /*
     * Enum variables
     */
    [SerializeField] private State currentState; // enemys current state
    [SerializeField] private EnemyType enemyType;

    /*
     * Raycast
     */
    Vector3 endPos;
    Vector3 endPos2;
    RaycastHit2D hit;
    RaycastHit2D infrontInfo;
    RaycastHit2D groundInfo;
    float castDist;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform firePoint2;

    // Start is called before the first frame update
    private void Start()
    {

    }
    // Update is called once per frame

    //returns the enemys current health
    public int GetHealth()
    {
        return currentHealth;
    }

    /**
     * Updates the enemy health
     * Params: the amount the health is changed.
     */
    public void UpdateHealth(int amount)
    {
        currentHealth += amount;
        print("Enemy Health: " + GetHealth());
    }


    //Enemy states
    public enum State
    {
        Idle,
        Attacking,
        Charging,
        CoolDown,
        Dead
    }

    public enum EnemyType
    {
        Boss,
        ChargeNPC,
        CloudBomber
    }
    public EnemyType GetEnemyType()
    {
        return enemyType;
    }
    public void SetState(State state)
    {
        currentState = state;
        HandleNewState(state);
    }

    //returns the enemys current state
    public State GetState()
    {
        return currentState;
    }

    //Handles the enemys new state
    private void HandleNewState(State state)
    {
        //switch statement to handle various states
        switch (state)
        {
            //if the enemys state is attacking
            case State.Attacking:
                break;
            //if the enemy is on cooldown
            case State.CoolDown:

                break;
            //if the enemy is dead
            case State.Dead:
                Destroy(this.gameObject); // The enemy is destroyed.
                break;
        }
    }
}