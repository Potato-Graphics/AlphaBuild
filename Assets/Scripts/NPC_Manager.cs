using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Manager : MonoBehaviour
{
    private float currentHealth;
    [SerializeField] private float MAX_HEALTH = 0;
    [SerializeField] private bool killable = false;
    [SerializeField] private float bulletDamage = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth(MAX_HEALTH);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetHealth()
    {
        return currentHealth;
    }

   public void UpdateHealth(float amount)
    {
        currentHealth += amount;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        print("test collision " + col.gameObject.tag);
        if(col.gameObject.tag == "Bullet")
        {
            print("bullet collision");
            if(killable)
            {
                UpdateHealth(-bulletDamage);
                if (GetHealth() <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        print("this is the collision" + col.gameObject.tag);
        if (col.gameObject.tag == "Bullet")
        {
            print("bullet collision");
            if (killable)
            {
                UpdateHealth(-bulletDamage);
                if (GetHealth() <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
