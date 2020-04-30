using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    Player player;
    public int scoreMaxAmount = 100;
    public int scoreMinAmount = 40;


    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }
  void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            player.UpdateScore(Random.Range(scoreMinAmount, scoreMaxAmount));
            player.coins++;
            Destroy(gameObject);
        }
    }
}
