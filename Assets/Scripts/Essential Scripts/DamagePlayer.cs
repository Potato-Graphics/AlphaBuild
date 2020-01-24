using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] bool destroyable;
    [SerializeField] int damageAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
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
