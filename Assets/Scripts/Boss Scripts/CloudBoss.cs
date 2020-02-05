using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBoss : MonoBehaviour
{

    [SerializeField] private Player thePlayer;
    [SerializeField] private State currentState;
    public float moveSpeed;
    public float playerRange;
    private float distanceToPlayer;
    [SerializeField] private float distanceToCharge = 10;
    Vector3 playerPosition;
    Vector3 cloudPosition;
    float timePassed = 0;
    float knockingBackCooldown = 10;
    bool followPlayer = false;
    bool knockingBack = false;
    [SerializeField] Rigidbody2D rb = null;

    // Gets the player game object.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        thePlayer = FindObjectOfType<Player>();
        SetState(State.Idle);
    }

    // Follows the player's position.
    void Update()
    {
        playerPosition = thePlayer.transform.position;
        cloudPosition = transform.position;
        distanceToPlayer = Vector3.Distance(playerPosition, cloudPosition);
        timePassed += Time.deltaTime;
        knockingBackCooldown += Time.deltaTime;

        if (GetState() != State.Charging)
            followPlayer = false;

        if (followPlayer)
            transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);


        if (knockingBack)
        {
            Vector3 knockbackAmount = new Vector3(6, 5, 0);
            Vector3 knockback = transform.position + knockbackAmount;
            transform.position = Vector2.MoveTowards(transform.position, knockback, moveSpeed * Time.deltaTime);

            if (timePassed > 4.5)
            {
                knockingBack = false;
                SetState(State.Idle);
                knockingBackCooldown = 0;
            }
        }
        if (GetState() != State.Knockedback)
        {
            if (distanceToPlayer < distanceToCharge && GetState() != State.Charging)
            {
                SetState(State.Charging);
            }
            if (distanceToPlayer >= distanceToCharge && GetState() != State.Idle)
                SetState(State.Idle);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (col.gameObject.tag == "Bullet")
        {
            if (GetState() != State.Knockedback)
            {
                Knockback();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
            rb.constraints = RigidbodyConstraints2D.None;
    }

    public enum State
    {
        Idle,
        Charging,
        Knockedback,
        Dead
    }

    public State GetState()
    {
        return currentState;
    }

    public void SetState(State state)
    {
        if (state == currentState)
            return;
        currentState = state;
        HandleNewState(state);
    }

    private void HandleNewState(State state)
    {
        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Charging:
                FollowPlayer();
                break;
            case State.Knockedback:
                knockingBack = true;
                break;
        }
    }

    void FollowPlayer()
    {
        followPlayer = true;
    }

    public void Knockback()
    {
        if (knockingBackCooldown > 4)
        {
            SetState(State.Knockedback);
            timePassed = 0;
        }
    }

    void Idle()
    {
        rb.velocity = Vector3.zero;
    }
}