using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    // Start is called before the first frame update
    float moveSpeed = 7f;

    Rigidbody2D rb;

    Player target;
    Vector2 moveDirection;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<Player>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy (gameObject, 3f);
    }

    // Update is called once per frame
    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.name.Equals ("player"))
        {
           
            Destroy(gameObject);
        }
        


        
    }
}
