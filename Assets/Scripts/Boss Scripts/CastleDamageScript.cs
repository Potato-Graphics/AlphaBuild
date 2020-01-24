using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleDamageScript : MonoBehaviour
{
    private int currentHealth;

    void Start()
    {
        currentHealth = 50;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DamageCastle(int damage)
    {
        currentHealth -= damage;
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
