using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_onCollision : MonoBehaviour
{

    [SerializeField] Player player;

    //Handles the bullet collision
  
    private void OnCollisionEnter(Collision col)
    {
        print("Collision Test 1");

        if (col.gameObject.tag != "Player" || col.gameObject.tag.Contains("Bullet"))

            print("Collision test 2");
        {
            //destroy the bullet.
            Destroy(gameObject);
        }
    }
}
