using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    Player player;
    public int scoreAmount = 75;


    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }
  void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            player.UpdateScore(scoreAmount);
            player.coins++;
            Destroy(gameObject);
        }
    }
}
