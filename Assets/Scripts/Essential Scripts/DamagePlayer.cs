using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] bool destroyable;
    [SerializeField] int damageAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        print("test" + col.gameObject.tag);
        if (col.gameObject.tag.Equals("Player"))
        {
            player.DealDamage(damageAmount);

            if (destroyable)
            {
                Destroy(gameObject);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        print("test" + col.gameObject.tag);
        if (col.gameObject.tag.Equals("Player"))
        {

            player.DealDamage(damageAmount);

            if (destroyable)
            {
                Destroy(gameObject);
            }
        }
    }
}
