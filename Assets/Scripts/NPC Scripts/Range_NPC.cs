using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range_NPC : MonoBehaviour
{
    private State currentState; // enemys current state
    [SerializeField] private float attackSpeed = 10;
    Player player;
    Vector3 targetLocation;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        SetState(State.Idle);

    }

    // Update is called once per frame
    void Update()
    {
        print(transform.position.x - player.transform.position.x);
        if(transform.position.x - player.transform.position.x <= 10 && GetState() != State.Attacking)
        {
            SetState(State.Attacking);
        }
        if(GetState() == State.Attacking)
        {
            targetLocation.y = -0.3f;
            if (transform.position == targetLocation)
            {
                SetState(State.Dead);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, targetLocation, attackSpeed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.DealDamage(1);
            SetState(State.Dead);
        }
    }

    public enum State
    {
        Idle,
        Attacking,
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
            case State.Idle:
                break;
            //if the enemys state is attacking
            case State.Attacking:
                targetLocation = player.transform.position;
                targetLocation.x = player.transform.position.x + Random.Range(2, 10);
                print(targetLocation);
                print(player.transform.position);
                break;
            case State.Dead:
                Destroy(this.gameObject); // The enemy is destroyed.
                break;
        }
    }
}
