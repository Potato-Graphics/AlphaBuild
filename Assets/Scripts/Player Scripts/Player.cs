using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Controller2D))]

public class Player : MonoBehaviour
{

    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;

    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    public float wallStickTime = .25f;
    public float timeToWallUnstick;
    public float wallSlideSpeedMax = 3;
    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;
    public Transform zipline;

    static int ID = 0;

    float accelerationTimeGrounded = .1f;
    float accelerationTimeAirborne = .3f;
    float moveSpeed = 12;
    float gravity = -30;
    float jumpVelocity;
    float velocityXSmoothing;

    public float coins = 0;

    public bool movingRight = false;
    public bool rotation = false;

    [SerializeField] private const int MAX_HEALTH = 3;
    [SerializeField] private static int score = 0;
    public int currentHealth;
    ///*public */[SerializeField] bool isAttackable = true;
    public static bool isAttackable = true;
    public float direction;
    [SerializeField] GameObject lifeOne = null;
    [SerializeField] GameObject lifeTwo = null;
    [SerializeField] GameObject lifeThree = null;
    public static Vector3 spawnLocation = new Vector3(-52.7122f, 4.03075f, -0.3430906f);
    [SerializeField] GameObject player;
    public static Vector3 checkpointPos;
    [SerializeField] float dashDistance = 4f;
    public static int checkpointsReceived;
    public static int waterRemaining;
    float dashSpeed = 150.0f;
    [SerializeField]GameObject endPoint;

    Vector3 lastMouseCoord = Vector3.zero;
    bool movedUp = false;
    bool movedDown = false;
    public static int totalPumps = 0;
    public static float bulletDamage = 1f;
    bool pumpStarted = false;
    public float airTimeJumpDelay;
    public bool canJump = false;

    public int sceneToRespawnOn;

    public bool ridingZipline = false;
    public bool usingController = true;
    Vector3 playerPosition;

    Vector3 velocity;

    float cooldown;

    Controller2D controller;

    public int myID;

    Vector2 dashPosition;

    public float bulletSizeMultiplier = 1;


    private Animator anim;

    [SerializeField]public Image specialBar;

    // Start is called before the first frame update
    void Awake()
    {

    }

    void Start()
    {
        anim = player.GetComponent<Animator>();

        waterRemaining = 50;
        DontDestroyOnLoad(gameObject);
        print("test1");

        print(checkpointsReceived);
        print("checkpoint pos " + checkpointPos);
        totalPumps = 0;

        //transform.position = spawnLocation;
        sceneToRespawnOn = SceneManager.GetActiveScene().buildIndex;
        myID = ID++;
        lifeOne.SetActive(true);
        lifeTwo.SetActive(true);
        lifeThree.SetActive(true);
        controller = GetComponent<Controller2D>();
        isAttackable = true;
        specialBar.fillAmount = 0;

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    void OnEnable()
    {
        GameManager.OnGameOver += HandleDeath;
    }

    void OnDisable()
    {
        GameManager.OnGameOver -= HandleDeath;
    }

    //Stops the player from moving building up downward force when standing still.
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.LogError(GameManager.respawnEnemies.Count);
        }
        playerPosition = transform.position;
        //JAM CODE
        /////////////////////////////////
        if (controller.collisions.below)
        {
            anim.SetBool("IsGrounded", true);
        }
        else
        {
            anim.SetBool("IsGrounded", false);
        }
        ///////////////////////////////

        //Gets the inputs for moving left and right.
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        int wallDirX = (controller.collisions.left) ? -1 : 1;

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        anim.SetFloat("Speed", Mathf.Abs(velocity.x / moveSpeed));

        if (Input.GetAxisRaw("Fire2") != 0)
        {
            pumpStarted = true;
            Vector3 mouseDelta = Input.mousePosition - lastMouseCoord;
            if (mouseDelta.y > 10 && movedUp == false)
            {
                if (totalPumps >= 10) return;
                totalPumps++;
                if (specialBar.fillAmount >= 1.0) return;
                specialBar.fillAmount += 0.1f;
                bulletSizeMultiplier += 0.1f;
                movedUp = true;
                movedDown = false;
            }
            if (mouseDelta.y < -10 && movedDown == false)
            {
                if (totalPumps >= 10) return;
                if (specialBar.fillAmount >= 1.0) return;
                specialBar.fillAmount += 0.1f;
                bulletSizeMultiplier += 0.1f;
                totalPumps++;
                movedDown = true;
                movedUp = false;
            }
            lastMouseCoord = Input.mousePosition;
        }
        if (Input.GetAxisRaw("Fire2") == 0 && pumpStarted)
        {
            pumpStarted = false;
            bulletDamage += totalPumps / 10;
            totalPumps = 0;
            movedDown = false;
            movedUp = false;
            
        }

        bool wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
            {
                {
                    velocity.y = -wallSlideSpeedMax;
                }

                if (timeToWallUnstick > 0)
                {
                    velocityXSmoothing = 0;
                    velocity.x = 0;

                    if (input.x != wallDirX && input.x != 0)
                    {
                        timeToWallUnstick -= Time.deltaTime;
                    }
                    else
                    {
                        timeToWallUnstick = wallStickTime;
                    }
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }

            }
        }

        if(ridingZipline)
        {
            playerPosition = zipline.position;
            playerPosition.y += 2.2f;
            transform.position = playerPosition;
            anim.SetBool("ridingZipline", true);
        }
        else
        {
            anim.SetBool("ridingZipline", false);
        }

        //Player Dash
        if(controller.dashing)
        {
            transform.position = Vector3.MoveTowards(transform.position, dashPosition, dashSpeed * Time.deltaTime);
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            SetHealth(10000);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (ridingZipline) return;
            if (!controller.canDash)
                return;
            if (controller.facingRight)
            {
                
                dashPosition = transform.position;
                dashPosition.x += dashDistance;
                controller.dashing = true;
                controller.canDash = false;
                controller.StartCoroutine(controller.DashDelay());
                controller.StartCoroutine(controller.Dashing());
            }
            else
            {
                dashPosition = transform.position;
                dashPosition.x += dashDistance;
                controller.dashing = true;
                controller.canDash = false;
                controller.StartCoroutine(controller.DashDelay());
                controller.StartCoroutine(controller.Dashing());
            }
        }
        if (!controller.collisions.below)
        {
            airTimeJumpDelay += Time.deltaTime;
            if(airTimeJumpDelay > 0.2)
            {
                canJump = false;
            }
        }
        else
        {
            airTimeJumpDelay = 0;
            canJump = true;
        }
        

            if (GetHealth() <= 0)
        {
            lifeOne.SetActive(false);
            lifeTwo.SetActive(false);
            lifeThree.SetActive(false);
            HandleDeath();
        }
        if (GetHealth() == 1)
        {
            lifeOne.SetActive(false);
            lifeTwo.SetActive(false);
            lifeThree.SetActive(true);
        }
        if (GetHealth() == 2)
        {
            lifeOne.SetActive(false);
            lifeTwo.SetActive(true);
            lifeThree.SetActive(true);
        }
        if (GetHealth() == 3)
        {
            lifeOne.SetActive(true);
            lifeTwo.SetActive(true);
            lifeThree.SetActive(true);
        }
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        //Gets the inputs for moving left and right.

        if (input.x < 0)
        {
            direction = -1;
            movingRight = false;
        }
        else if (input.x > 0)
        {
            direction = 1;
            movingRight = true;
        }
        else { direction = 0; }

        if ((Input.GetButtonDown("Jump")))
        {
            if (ridingZipline) return;
            if (wallSliding)
            {
                if (wallDirX == input.x)
                {
                    anim.SetTrigger("Jump");
                    velocity.x = -wallDirX * wallJumpClimb.x;
                    velocity.y = wallJumpClimb.y;
                }
                else if (input.x == 0)
                {
                    anim.SetTrigger("Jump");

                    velocity.x = -wallDirX * wallJumpOff.x;
                    velocity.y = wallJumpOff.y;
                }
                else
                {
                    anim.SetTrigger("Jump");
                    velocity.x = -wallDirX * wallLeap.x;
                    velocity.y = wallLeap.y;
                }
            }
            if (canJump)
            {
                anim.SetTrigger("Jump");
                velocity.y = jumpVelocity;
            }
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //Gets key input for shooting upwards.
        if (Input.GetKeyDown(KeyCode.W))
        {
            rotation = true;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            rotation = false;
        }

    }

    void HandleDeath()
    {
        OnPlayerDied();
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void UpdateHealth(int amount)
    {
        currentHealth += amount;

    }

    public void SetHealth(int amount)
    {
        currentHealth = amount;
    }

    public int GetScore()
    {
        return score;
    }

    public void UpdateScore(int amount)
    {
        score += amount;
    }

    public void DealDamage(int amount)
    {
        if (isAttackable == true)
        {
            UpdateHealth(-amount);
            isAttackable = false;
            StartCoroutine(DamagedDelay());
        }
    }

    void Reload()
    {

    }

    public void SendReloadText()
    {
        //TODO: Send reload text ui
    }

    IEnumerator DamagedDelay()
    {
        yield return new WaitForSeconds(2);
        isAttackable = true;
    }
}
