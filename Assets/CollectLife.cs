using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectLife : MonoBehaviour
{
    Player player;
    Rigidbody2D rb;
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Obstacles")
        {
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0f;
        }
        if (col.gameObject.tag == "Player")
        {
            if (player.GetHealth() < 3)
            {
                player.UpdateHealth(1);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("HEALTH IS FULL" +player.GetHealth());
            }
        }
    }
}
