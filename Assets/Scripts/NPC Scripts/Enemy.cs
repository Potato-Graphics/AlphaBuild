using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int distanceToCharge = 6; // distanced required for the enemy to charge
    private const int MAX_HEALTH = 25;
    private int currentHealth; // enemys current health
    [SerializeField] private float walkSpeed = 10.0f; // charge speed
    [SerializeField] private float chargeSpeed = 50.0f;
    private Transform playerObject; // the player transform object
    [SerializeField] private Vector3 playerPosition; // players position
    private Vector2 dir = new Vector2(-1, 0);
    private Vector3 startPosition;
    private float distance; // distance between enemy and player
    private Vector3 enemyPosition; // enemy position
    [SerializeField]private State currentState; // enemys current state
    private Player player; // the player object
    [SerializeField] private float idleWalkDistance = 5.0f;
    [SerializeField] private EnemyType enemyType; //the enemy type
    private bool movingRight = true;
    [SerializeField] public bool collidingWithPlayer = false;
    Vector3 targetLocation;
    Vector3 forceVector;
    Rigidbody2D rb;
    private float timePassed;
    int bounceRange;
    private Vector3 localScale;

    private bool shouldLerp = false;

    public Vector2 endPosition;

    public float timeStartedLerping;
    public float lerpTime;

    /*
     * Raycast
     */
    Vector3 endPos;
    Vector3 endPos2;
    Vector3 endPos3;
    RaycastHit2D hit;
    RaycastHit2D hit2;
    RaycastHit2D infrontInfo;
    RaycastHit2D groundInfo;
    float castDist;
    float castDist2;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform firePoint2;
    [SerializeField] GameObject bubblePrefab;
    public int bubblesSpawned;


    public Vector3 Lerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime = 1)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;

        float percentageComplete = timeSinceStarted / lerpTime;

        var result = Vector3.Lerp(start, end, percentageComplete);

        return result;
    }

    private void StartLerping()
    {
        timeStartedLerping = Time.time;

        shouldLerp = true;
        
    }
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        currentHealth = MAX_HEALTH;
        //Sets the enemy to idle on start
        localScale = transform.localScale;
        startPosition = transform.position;
        endPos.z = -2.24f;

        rb = GetComponent<Rigidbody2D>();
        SetState(State.Idle);
        startPosition = transform.position;
        StartLerping();

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        print(col.gameObject.name);
        if(col.gameObject.tag == "Obstacles")
        {
            if(GetEnemyType() == EnemyType.BounceNPC)
            {
                print("test here");
                rb.AddForce(new Vector2(1, 300));
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        bounceRange = Random.Range((int)player.transform.position.x - 5, (int)player.transform.position.x + 5);
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
        if(GetEnemyType() == EnemyType.BounceNPC)
        {
            groundInfo = Physics2D.Raycast(firePoint2.position, Vector2.down, 4f);
        }


        if (GetEnemyType() == EnemyType.ObstructorNPC)
        {
            if(distance < 35)
            {
                if (GetState() == State.Idle)
                {
                    SetState(State.Attacking);
                }
            }
        }
        

        if(GetEnemyType() == EnemyType.ObstructorNPC)
        {
            if(GetState() == State.Attacking)
            {
                ObstructorAttack();
            }
        }

        if (currentHealth <= 0)
            //if the enemy has no remaining health the enemy is set to dead.
            SetState(State.Dead);
        if (GetEnemyType() == EnemyType.BounceNPC)
        {
            transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
            if (groundInfo.collider == false)
            {
                if(movingRight)
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
        if (GetEnemyType() == EnemyType.ChargeNPC)
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


            if (GetState() == State.Charging)
            {
                if (timePassed > 5)
                    SetState(State.Idle);
                targetLocation = player.transform.position;
                targetLocation.y = transform.position.y;
                transform.position = Vector3.MoveTowards(transform.position, targetLocation, (chargeSpeed * Time.deltaTime));
            }

            if (GetState() == State.Idle)
            {
                transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
            }
        }
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

    IEnumerator BubbleCoolDown()
    {
        yield return new WaitForSeconds(5);
        SetState(State.Idle);
    }

    private void ObstructorAttack()
    {
        int bubblesToBeSpawned = Random.Range(6, 13);
        if(bubblesSpawned < bubblesToBeSpawned)
        {
            Instantiate(bubblePrefab, transform.position, Quaternion.identity);
            bubblesSpawned++;
        } else
        {
            SetState(State.CoolDown);
            StartCoroutine(BubbleCoolDown());
        }
    }
    bool CanSeePlayer(float distance)
    {
        bool val = false;
        castDist = distance;
        castDist2 = -distance;

        if (GetEnemyType() != EnemyType.ObstructorNPC)
        {
            if (!movingRight)
            {
                castDist = -distance;
            }
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

    //Handles the enemy bounce attack.
    private void Bounce()
    {
        SetState(State.Bouncing);
    }

    //Handles the enemy charge attack.
    private void Charge()
    {
        if (GetState() == State.CoolDown)
            return;
        if (GetState() == State.Charging)
            return;
        timePassed = 0;

        //Enemy is set to the charging state
        SetState(State.Charging);
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
        Bouncing,
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
                if (GetEnemyType() == EnemyType.BounceNPC)
                    Bounce();
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
                print("dead");
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
        ObstructorNPC,
        ChargeNPC,
        BounceNPC,
    }

    public EnemyType GetEnemyType()
    {
        return enemyType;
    }
}