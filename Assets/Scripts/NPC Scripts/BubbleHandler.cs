using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleHandler : MonoBehaviour
{
    State currentState;
    public Rigidbody2D rb;
    Player player;
    float idleSpeed = 100;
    float chaseSpeed = 5;
    Enemy enemy;
    public int pointsGiven;

    private int currentPathIndex;
    private List<Vector3> pathVectorList;


    // Start is called before the first frame update
    void Start()
    {
        SetState(State.Idle);
        enemy = GameObject.FindObjectOfType<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyBubbleDelay());
        player = GameObject.FindObjectOfType<Player>();
        SetTargetPosition();



    }

    // Update is called once per frame
    void Update()
    {
        if(GetState() == State.Attacking)
        {
            HandleChase();
        }
    }

    private void HandleChase()
    {
        if(pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if(Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                Vector3 moveDir = (targetPosition - targetPosition).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + moveDir * chaseSpeed * Time.deltaTime;
            } else
            {
                currentPathIndex++;
                if(currentPathIndex >= pathVectorList.Count)
                {
                    SetState(State.Idle);
                    pathVectorList = null;
                }
            }
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

    private IEnumerator IdleTime()
    {
        yield return new WaitForSeconds(2.1f);
        SetState(State.Attacking);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.DealDamage(1);
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Bullet")
        {
            ScoreManager.scoreValue += pointsGiven;
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyBubbleDelay()
    {
        yield return new WaitForSeconds(15);
        Destroy(this.gameObject);
        enemy.bubblesSpawned--;
    }

    public void SetTargetPosition()
    {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath(transform.position, player.transform.position);

        if(pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }
    private void HandleNewState(State newState)
    {
        switch(newState)
        {
            case State.Idle:
                float randomX = Random.Range(-200, 200);
                float randomY = Random.Range(0, 200);
                Vector2 force = new Vector2(randomX, randomY);
                rb.AddForce(force);
                StartCoroutine(IdleTime());
                break;

            case State.Attacking:
                rb.velocity = Vector2.zero;
                break;
        }
    }
}
