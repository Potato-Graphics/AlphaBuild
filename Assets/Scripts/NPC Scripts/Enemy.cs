using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int distanceToCharge = 6; // distanced required for the enemy to charge
    [SerializeField] private const int MAX_HEALTH = 25;
    private int currentHealth; // enemys current health
    [SerializeField] private float walkSpeed = 10.0f; // charge speed
    [SerializeField] private float chargeSpeed = 50.0f;
    [SerializeField] private Transform playerObject; // the player transform object
    [SerializeField] private Vector3 playerPosition; // players position
    private Vector2 dir = new Vector2(-1, 0);
    private Vector3 startPosition;
    [SerializeField] private float distance; // distance between enemy and player
    [SerializeField] private Vector3 enemyPosition; // enemy position
    [SerializeField] private State currentState; // enemys current state
    [SerializeField] private Player player; // the player object
    [SerializeField] private float idleWalkDistance = 5.0f;
    [SerializeField] private EnemyType enemyType; //the enemy type
    [SerializeField] private bool movingRight = true;
    [SerializeField] public bool collidingWithPlayer = false;
    public Transform spawnPoint;
    private Vector2 direction = new Vector2(-1, 0);
    Rigidbody2D rb;
    private float timePassed;
    private Vector3 localScale;

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
        currentHealth = MAX_HEALTH;
        //Sets the enemy to idle on start
        localScale = transform.localScale;
        startPosition = transform.position;
        endPos.z = -2.24f;

        rb = GetComponent<Rigidbody2D>();
        SetState(State.Idle);
        startPosition = transform.position;

    }
    // Update is called once per frame
    void Update()
    {
        // Initialising the player object
        playerObject = GameObject.FindGameObjectWithTag("Player").transform;
        //Initiliasing the players position
        playerPosition = playerObject.transform.position;
        //Initiliasing the enemy position
        enemyPosition = transform.position;
        //Initiliasing the distance between the enemy and player vector
        distance = Vector2.Distance(playerPosition, enemyPosition);
        timePassed += Time.deltaTime;

        endPos = firePoint.position + Vector3.right * castDist;
        hit = Physics2D.Linecast(firePoint.position, endPos);
        Debug.DrawLine(firePoint.position, endPos, Color.blue);
        groundInfo = Physics2D.Raycast(firePoint2.position, Vector2.down, 2f);
        infrontInfo = Physics2D.Raycast(firePoint.position, Vector2.right, 0.3f);

        endPos2 = firePoint2.position + Vector3.right * 0.01f;
        groundInfo = Physics2D.Raycast(firePoint2.position, Vector2.down, 2f);

        if(GetState() == State.Idle)
        {
            if (groundInfo.collider == false)
            {
                if (movingRight)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }
            }
            if (infrontInfo.collider == true)
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.tag != "Player")
                    {
                        if (movingRight)
                        {
                            transform.eulerAngles = new Vector3(0, -180, 0);
                            movingRight = false;
                        }
                        else
                        {
                            transform.eulerAngles = new Vector3(0, 0, 0);
                            movingRight = true;
                        }
                    }
                }
            }
            if (enemyPosition.x > startPosition.x + idleWalkDistance && movingRight)
            {
                movingRight = false;
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            if (enemyPosition.x <= startPosition.x - idleWalkDistance && !movingRight)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }

            if (CanSeePlayer(distanceToCharge))
            {
                
                SetState(State.Attacking);
            }
            else
            {
                
                SetState(State.Idle);
            }
        }
        

        if (GetState() == State.Charging)
            if (timePassed > 5)
                SetState(State.Idle);

        if (GetState() == State.Idle)
        {
            transform.Translate(Vector2.right * walkSpeed * Time.deltaTime);
        }

        if (currentHealth <= 0)
            //if the enemy has no remaining health the enemy is set to dead.
            SetState(State.Dead);
    }


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
    //Handles the delay when the enemy is set to cool down.
    IEnumerator DamageDelay()
    {
        //Enemy is set to idle after waiting for 2 seconds.
        yield return new WaitForSeconds(2);
        SetState(State.Idle);
    }

    bool CanSeePlayer(float distance)
    {
        bool val = false;
        castDist = distance;


        if(!movingRight)
        {
            castDist = -distance;
        }

        if (hit.collider != null)
        {
            Debug.DrawLine(firePoint.position, endPos, Color.red);
            if (hit.collider.gameObject.tag.Equals("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }

        }
        return val;
    }

    void RotateEnemy()
    {
        Vector3 locScale = transform.localScale;
        locScale.x *= -1;
        transform.localScale = locScale;
    }

    //Handles the enemy charge attack.
    private void Charge()
    {
        if (GetState() == State.CoolDown)
            return;
        if (GetState() == State.Charging)
            return;
        timePassed = 0;

        //Moves the enemy towards the player
        if(enemyPosition.x < playerPosition.x)
        {
            rb.velocity = new Vector2(chargeSpeed, 0);
            movingRight = true;
        }
        else
        {
            rb.velocity = new Vector2(-chargeSpeed, 0);
            movingRight = false;
        }
        //Enemy is set to the charging state
        SetState(State.Charging);
    }

    /*
     * Cloud Bomber functions
     * 
     */

    private void HandleCloudBomberPath()
    {

    }



    /*
     * State Functions
     */
    /* Sets the state of the enemy
  * Params: the new state being assigned to enemy
  */

        //Handles the enemys state
    public enum State
    {
        Idle,
        Attacking,
        Charging,
        CoolDown,
        Dead
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
                if (GetEnemyType() == EnemyType.ChargeNPC)
                    Charge(); // enemy does the charge attack if it's the charge npc
                break;
            //if the enemy is on cooldown
            case State.CoolDown:
                rb.velocity = Vector2.zero;
                StartCoroutine(DamageDelay()); // The enemys damage delay is started.
                break;
            //if the enemy is dead
            case State.Dead:
                Destroy(this.gameObject); // The enemy is destroyed.
                break;
        }
    }



    /*
     * Enemy Type functions
     */
    //Handles the type of the enemy.
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
}