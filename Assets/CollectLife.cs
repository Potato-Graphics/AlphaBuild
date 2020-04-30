using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectLife : MonoBehaviour
{
    Player player;
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if(player.GetHealth() < 3)
            {
                player.UpdateHealth(1);
                Destroy(gameObject);
            }
        }
    }
}
