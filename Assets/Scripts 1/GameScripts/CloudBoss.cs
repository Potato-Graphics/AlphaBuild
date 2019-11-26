using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBoss : MonoBehaviour
{

    [SerializeField]private Player thePlayer;
    [SerializeField] private State currentState;
    public float moveSpeed;
    public float playerRange;
    private float distanceToPlayer;
    [SerializeField]private float distanceToCharge = 10;
    Vector3 playerPosition;
    Vector3 cloudPosition;
    float timePassed = 0;
    [SerializeField] Rigidbody2D rb;

    // Gets the player game object.
    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
        SetState(State.Idle);
    }

    // Follows the player's position.
    void Update()
    {
       // print(currentState);
       // print(GetState());
        playerPosition = thePlayer.transform.position;
        cloudPosition = transform.position;
        distanceToPlayer = Vector3.Distance(playerPosition, cloudPosition);
        timePassed += Time.deltaTime;

        if(GetState() == State.Knockedback && timePassed > 3)
        {
            SetState(State.Idle);
            print(timePassed);
        }

        if (GetState() != State.Knockedback)
        {
           // print("not knock back");
            if (distanceToPlayer < distanceToCharge && GetState() != State.Charging)
                SetState(State.Charging);
            if (distanceToPlayer >= distanceToCharge && GetState() != State.Idle)
                SetState(State.Idle);
        }
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
        currentState = state;
        HandleNewState(state);
        print("The new state is " + state);
    }

    private void HandleNewState(State state)
    {
        switch(state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Charging:
                FollowPlayer();
                break;
            case State.Knockedback:
                break;
        }
    }

    void FollowPlayer()
    {
       // transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
        Vector3 moveDirection = playerPosition - cloudPosition;
        rb.AddForce(moveDirection.normalized * moveSpeed * Time.deltaTime);
    }

    public void Knockback()
    {
        print("knockback funciton");
        SetState(State.Knockedback);
        Vector3 moveDirection = cloudPosition - playerPosition;
        rb.AddForce(moveDirection.normalized);
        timePassed = 0;
    }

    void Idle()
    {
        rb.velocity = Vector3.zero;
    }
}
