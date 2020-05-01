using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BubbleHandler : MonoBehaviour
{

    public AudioSource bubblepop;

    State currentState;
    public Rigidbody2D rb;
    Player player;
    Enemy enemy;
    public int pointsGiven;

    // Start is called before the first frame update
    void Start()
    {
        SetState(State.Idle);
        enemy = GameObject.FindObjectOfType<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindObjectOfType<Player>();

       

    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetHealth() <= 0)
        {
            Destroy(gameObject);
        }
      
        
    }

    enum State
    {
        Idle,
        Attacking
    }

    private State GetState()
    {
        return currentState;
    }

    private void SetState(State state)
    {
        this.currentState = state;
        HandleNewState(state);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.DealDamage(1);
            Destroy(gameObject);
            bubblepop.Play();
        }
        if(collision.gameObject.tag == "Bullet")
        {
            ScoreManager.scoreValue += pointsGiven;
            enemy.bubblesSpawned--;
            Destroy(gameObject);
            bubblepop.Play();
        }
    }

    private void HandleNewState(State newState)
    {
        switch(newState)
        {
            case State.Idle:
                break;

            case State.Attacking:
                break;
        }
    }
}
