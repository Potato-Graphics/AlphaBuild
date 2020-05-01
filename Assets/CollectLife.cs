using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CollectLife : MonoBehaviour
{
    public AudioSource playercollect;
    Player player;
    Rigidbody2D rb;
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyLife());
    }

    IEnumerator DestroyLife()
    {
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
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
                if(player.GetHealth() == 1)
                {
                    player.currentHealth = 2;
                }
                else
                 {
                    player.currentHealth = 3;
                 }
                playercollect.Play();
                Destroy(gameObject);
        }
    }
}
