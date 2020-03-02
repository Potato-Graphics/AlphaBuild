using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{

    Rigidbody2D rb;
    Enemy enemy;
    [SerializeField] public bool rightProjectile;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GameObject.FindObjectOfType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.explodingRight)
        {
            float randomValueY = Random.Range(1, 2);
            float randomValueX = Random.Range(1, 3);
            Vector3 force = new Vector3(randomValueX, randomValueY);
            rb.AddForce(force);
        }
        else
        {
            float randomValueY = Random.Range(1, 2);
            float randomValueX = Random.Range(-1, -3);
            Vector3 force = new Vector3(randomValueX, randomValueY);
            rb.AddForce(force);
        }
    }
}
