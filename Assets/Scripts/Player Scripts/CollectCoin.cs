using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public AudioSource playercollect;
    Player player;
    public int scoreMaxAmount = 100;
    public int scoreMinAmount = 40;
    Rigidbody2D rb;


    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
    }
  void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Obstacles")
        {
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0f;
        }
        if (col.gameObject.tag == "Player")
        {
            player.UpdateScore(Random.Range(scoreMinAmount, scoreMaxAmount));
            player.coins++;
            playercollect.Play();
            Destroy(gameObject);
        }
    }
}
