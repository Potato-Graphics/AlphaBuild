using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] Player player;
    Enemy enemy;
    [SerializeField] bool destroyable;
    [SerializeField] int damageAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        enemy = GetComponent<Enemy>();
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
                enemy.UpdateHealth(enemy.MAX_HEALTH);
                enemy.SetState(Enemy.State.Dead);

            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //  print("test" + col.gameObject.tag);
        if (col.gameObject.tag.Equals("Player"))
        {

            player.DealDamage(damageAmount);

            if (destroyable)
            {

                enemy.UpdateHealth(enemy.MAX_HEALTH);
                enemy.SetState(Enemy.State.Dead);

            }
        }
    }
}
