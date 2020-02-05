using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_onCollision : MonoBehaviour
{

    [SerializeField] Player player;

    //Handles the bullet collision
  
    private void OnCollisionEnter2D(Collision2D col)
    {
        print("the collision is " + col.gameObject.tag);
        if (col.gameObject.tag != "Player" || col.gameObject.tag.Contains("Bullet"))

        {
            //destroy the bullet.
            Destroy(gameObject);
        }
    }
}
