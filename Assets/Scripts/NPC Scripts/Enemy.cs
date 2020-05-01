using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public static List<Enemy> enemies = new List<Enemy>();

    NPC_Manager npcManager;

    public delegate void EnemyDelegate();
    public static event EnemyDelegate OnEnemyDied;

    public AudioSource chargerpatrol;
    public AudioSource chargerexplosion;
    public AudioSource planedive;
    public AudioSource bubblepop;
    public AudioSource frogcroak;
    public AudioSource ballbounce;
    


    [SerializeField] private int distanceToCharge = 6; // distanced required for the enemy to charge
    public int MAX_HEALTH = 25;
    private int currentHealth; // enemys current health
    [SerializeField] private float walkSpeed = 10.0f; // charge speed
    [SerializeField] private float chargeSpeed = 50.0f;
    private Transform playerObject; // the player transform object
    [SerializeField] private Vector3 playerPosition; // players position
    private Vector2 dir = new Vector2(-1, 0);
    public Vector3 startPosition;
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
    public GameObject pathfinding;
    [SerializeField] bool comingFromLeft;
    private float timePassed;
    int bounceRange;
    bool carSoundPlaying = false;
    private Vector3 localScale;

    private bool shouldLerp = false;

    public Vector2 endPosition;

    public float timeStartedLerping;
    public float lerpTime;
    public float twopie = Mathf.PI * 2;
    public float afloat = 0;
    public float osculationSpeed = 0.01f;
    public bool frogSoundPlaying = false;
    public bool planeSounddPlaying = false;

    BoxCollider2D boxCollider;
    CircleCollider2D circleCollider;

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
    [SerializeField] GameObject explosionPrefab;
    public int bubblesSpawned;
    public int explosionProjectilesSpawned;
    public bool explodingRight = false;
    public float jumpHeight = 300f;
    public float amount = 3f;
    public float fallSpeed = 10;
    public float spinSpeed = 100;
    public int startHealth;
    bool reached2Pi = false;
    bool canLaunchBubble = true;

    public AIPath aiPath;
    

    public int NPC_ID = 0;
    public GameObject enemyPrefab;



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
        
        aiPath = GetComponent<AIPath>();
        npcManager = GetComponent<NPC_Manager>();
        player = GameObject.FindObjectOfType<Player>();
        currentHealth = MAX_HEALTH;
        //Sets the enemy to idle on start
        localScale = transform.localScale;
        startPosition = transform.position;
        endPos.z = -2.24f;

        rb = GetComponent<Rigidbody2D>();
        SetState(State.Idle);
        startPosition = transform.position;
        startHealth = currentHealth;
        circleCollider = GetComponent<CircleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        enemies.Add(this);
     

    }
    void OnEnable()
    {
        GameManager.OnEnemyDeath += HandleDeath;
    }

    void OnDisable()
    {
        GameManager.OnEnemyDeath -= HandleDeath;
    }

    public void HandleDeath()
    {
        OnEnemyDied();
    }

    public void AddToRespawnList()
    {
        GameManager.Instance.AddRespawnObj(NPC_ID, startPosition, gameObject, startHealth);
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if(circleCollider != null)
            {
                if (col.gameObject.GetComponent<BoxCollider2D>() != null)
                {
                    Physics2D.IgnoreCollision(circleCollider, col.gameObject.GetComponent<BoxCollider2D>());
                }
                else
                    if (col.gameObject.GetComponent<CircleCollider2D>() != null)
                {
                    Physics2D.IgnoreCollision(circleCollider, col.gameObject.GetComponent<CircleCollider2D>());
                }



            } else
                if(boxCollider != null)
            {
                if (col.gameObject.GetComponent<BoxCollider2D>() != null)
                {
                    Physics2D.IgnoreCollision(circleCollider, col.gameObject.GetComponent<BoxCollider2D>());
                }
                else
                      if (col.gameObject.GetComponent<CircleCollider2D>() != null)
                {
                    Physics2D.IgnoreCollision(circleCollider, col.gameObject.GetComponent<CircleCollider2D>());
                }
            }
           Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), col.gameObject.GetComponent<BoxCollider2D>());
            if (GetEnemyType() == EnemyType.HelicopterSeed)
            {
                transform.position = startPosition;
            }
        }
        if(col.gameObject.tag == "Obstacles")
        {
            if(GetEnemyType() == EnemyType.RangePlane)
            {
                //Destroy(gameObject);
                SetHealth(MAX_HEALTH);
                AddToRespawnList();
                SetState(State.Dead);
                gameObject.SetActive(false);
            }
            if (GetEnemyType() == EnemyType.HelicopterSeed)
            {
                transform.position = startPosition;
            }
            if (GetEnemyType() == EnemyType.BounceStressBall)
            {
                if(Vector3.Distance(transform.position, player.transform.position) < 25)
                {
                    ballbounce.Play();
                }
                rb.AddForce(new Vector2(1f, jumpHeight));
            }
        }
    }
    IEnumerator CarSoundDelay()
    {
        yield return new WaitForSeconds(7);
        carSoundPlaying = false;
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
        if (GetEnemyType() != EnemyType.HelicopterSeed)
        {
            endPos = firePoint.position + Vector3.right * castDist;
            hit = Physics2D.Linecast(firePoint.position, endPos);
            Debug.DrawLine(firePoint.position, endPos, Color.blue);
            groundInfo = Physics2D.Raycast(firePoint2.position, Vector2.down, 2f);
            infrontInfo = Physics2D.Raycast(firePoint.position, Vector2.right, 0.3f);
            Debug.DrawLine(firePoint2.position, endPos3, Color.cyan);

            endPos2 = firePoint2.position + Vector3.right * 0.01f;
            endPos3 = firePoint2.position + Vector3.down;
        }

        
        if(afloat < twopie)
        {
            afloat += osculationSpeed;
        }
        else
        {
            afloat = 0;
        }

        if(GetEnemyType() == EnemyType.HelicopterSeed)
        {
            Vector3 aVector = new Vector3(startPosition.x + Mathf.Sin(afloat) * amount, startPosition.y, startPosition.z);
            aVector.y = enemyPosition.y -= Time.deltaTime * fallSpeed;
            transform.position = aVector;
            transform.Rotate(new Vector3(0, spinSpeed * Time.deltaTime, 0));
        }
        


        switch(GetEnemyType())
        {
            case EnemyType.RangePlane:

                float planeDistance = Vector3.Distance(player.transform.position, transform.position);
                if (planeDistance < distanceToCharge)
                {
                    SetState(State.Attacking);
                }
                if (GetState() == State.Attacking)
                {
                    aiPath.enabled = true;
                    planedive.Play();
                    planeSounddPlaying = true;
                    StartCoroutine(PlaneSoundDelay());
                }
                break;


            case EnemyType.BounceStressBall:
                transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
                groundInfo = Physics2D.Raycast(firePoint2.position, Vector2.down, 5f);
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
                break;

            case EnemyType.ChargeCar:
            case EnemyType.ChargeBuggy:
                if(GetState() == State.Charging || GetState() == State.Attacking)
                {
                    if(!carSoundPlaying)
                    {
                        chargerpatrol.Play();
                        carSoundPlaying = true;
                        StartCoroutine(CarSoundDelay());
                    }
                }
                if (groundInfo.collider == false)
                {
                    //Debug.LogError("false");
                    if (movingRight)
                    {
                       // Debug.LogError("test three");
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
                               // Debug.LogError("test two");
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
//                    Debug.LogError("test one");
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
                break;


            case EnemyType.ObstructorFrog:
                if (distance < 35)
                {
                    if (GetState() == State.Idle)
                    {
                        SetState(State.Attacking);
                    }
                }
                if (GetState() == State.Attacking)
                {
                    ObstructorAttack();
                }
                break;

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

    public void RangeExplosion()
    {
        
            
    }

    public void SetHealth(int amount)
    {
        currentHealth = amount;
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

    IEnumerator BubbleDelay()
    {
        yield return new WaitForSeconds(0.2f);
        canLaunchBubble = true;
    }

    IEnumerator FrogSoundDelay()
    {
        yield return new WaitForSeconds(129);
        frogSoundPlaying = false;
    }

    IEnumerator PlaneSoundDelay()
    {
        yield return new WaitForSeconds(20f);
        planeSounddPlaying = false;
    }

    private void ObstructorAttack()
    {
        int bubblesToBeSpawned = Random.Range(6, 13);
        float randomValue = Random.Range(0.05f, 0.2f);
        Vector3 randomSize = new Vector3(randomValue, randomValue);
        Vector3 spawnPosition = transform.position;
        spawnPosition.x = transform.position.x - 2f;
        spawnPosition.y = transform.position.y + Random.Range(0, 1);
        if(bubblesSpawned < bubblesToBeSpawned)
        {
            if(canLaunchBubble)
            {
                GameObject bubbles = bubblePrefab;
                bubbles.transform.localScale = randomSize;
                Instantiate(bubbles, spawnPosition, Quaternion.identity);
                bubblesSpawned++;
                canLaunchBubble = false;
                if(!frogSoundPlaying)
                {
                    bubblepop.Play();
                    StartCoroutine(FrogSoundDelay());
                }
                StartCoroutine(BubbleDelay());
            }
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

        if (GetEnemyType() != EnemyType.ObstructorFrog)
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
                switch(GetEnemyType())
                {
                    case EnemyType.RangePlane:

                        break;
                    case EnemyType.BounceStressBall:
                        Bounce();
                        break;
                    case EnemyType.ChargeCar:
                    case EnemyType.ChargeBuggy:
                        Charge();
                        break;
                }
                break;
            //if the enemy is on cooldown
            case State.CoolDown:
                rb.velocity = Vector2.zero;
                StartCoroutine(DamageDelay()); // The enemys damage delay is started.
                break;
            //if the enemy is dead
            case State.Dead:
                if (GetEnemyType() == EnemyType.BounceStressBall)
                {
                    if (GetEnemyType() == EnemyType.BounceStressBall)
                    {
                        bubblepop.Play();
                    }
                    if (GetEnemyType() == EnemyType.ChargeBuggy)                   {
                        chargerexplosion.Play();
                    }
                    if (GetEnemyType() == EnemyType.ChargeCar)
                    {
                        chargerexplosion.Play();
                    }
                }

                //Destroy(this.gameObject); // The enemy is destroyed.
                if (GetEnemyType() == EnemyType.HelicopterSeed)
                {
                    transform.position = startPosition;
                    SetHealth(MAX_HEALTH);
                    SetState(State.Idle);
                }
                else
                {
                    SetHealth(MAX_HEALTH);
                    AddToRespawnList();
                    SetState(State.Idle);
                    npcManager.healthBar.fillAmount = 1;
                    gameObject.SetActive(false);
                }
                break;
        }
    }



    /*
     * Enemy Type functions
     */
    //Handles the type of the enemy.
    public enum EnemyType
    {
        ObstructorFrog,
        ChargeCar,
        ChargeBuggy,
        BounceStressBall,
        RangePlane,
        HelicopterSeed
    }

    public EnemyType GetEnemyType()
    {
        return enemyType;
    }
}