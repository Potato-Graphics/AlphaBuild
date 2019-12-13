using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD:Assets/Scripts/GameScripts/Player.cs
[RequireComponent (typeof (Controller2D))]
=======
[RequireComponent(typeof(Controller2D))]
>>>>>>> remotes/origin/Ollie:Assets/Scripts/Player.cs

public class Player : MonoBehaviour
{
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
<<<<<<< HEAD:Assets/Scripts/GameScripts/Player.cs

=======
>>>>>>> remotes/origin/Ollie:Assets/Scripts/Player.cs
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 8;
    float gravity;
    float jumpVelocity;
    float velocityXSmoothing;
    public bool movingRight = false;
    public bool rotation = false;
    [SerializeField] private const int MAX_HEALTH = 3;
    [SerializeField] private int score = 0;
    public int currentHealth;
    public bool isAttackable = true;
<<<<<<< HEAD:Assets/Scripts/GameScripts/Player.cs

=======
>>>>>>> remotes/origin/Ollie:Assets/Scripts/Player.cs
    public float direction;

    Vector3 velocity;

    Controller2D controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + " Jump Velocity: " + jumpVelocity);
    }

    //Stops the player from moving building up downward force when standing still.
    void Update()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        //Gets the inputs for moving left and right.
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

<<<<<<< HEAD:Assets/Scripts/GameScripts/Player.cs
        if(input.x < 0)
=======
        if (input.x < 0)
>>>>>>> remotes/origin/Ollie:Assets/Scripts/Player.cs
        {
            direction = -1;
            movingRight = false;
        }
<<<<<<< HEAD:Assets/Scripts/GameScripts/Player.cs
        else if(input.x > 0)
=======
        else if (input.x > 0)
>>>>>>> remotes/origin/Ollie:Assets/Scripts/Player.cs
        {
            direction = 1;
            movingRight = true;
        }
        else { direction = 0; }

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeGrounded);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //Gets key input for shooting upwards.
        if (Input.GetKeyDown(KeyCode.W))
        {
            rotation = true;
        }
<<<<<<< HEAD:Assets/Scripts/GameScripts/Player.cs
        if(Input.GetKeyUp(KeyCode.W))
        {
            rotation = false;
        }

=======
        if (Input.GetKeyUp(KeyCode.W))
        {
            rotation = false;
        }
>>>>>>> remotes/origin/Ollie:Assets/Scripts/Player.cs
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void UpdateHealth(int amount)
    {
        currentHealth += amount;

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
        if (!isAttackable)
            return;
        UpdateHealth(-amount);
        print("Player Health: " + GetHealth());
        isAttackable = false;
    }

<<<<<<< HEAD:Assets/Scripts/GameScripts/Player.cs

=======
>>>>>>> remotes/origin/Ollie:Assets/Scripts/Player.cs
    IEnumerator DamagedDelay()
    {
        yield return new WaitForSeconds(2);
        isAttackable = true;
    }
}
