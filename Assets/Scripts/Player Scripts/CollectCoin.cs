using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    Player player;


    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }
  void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            player.coins += 1;
            Destroy(gameObject);
        }
    }
}
