using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Manager : MonoBehaviour
{
    private float currentHealth;
    [SerializeField] private float MAX_HEALTH = 0;
    [SerializeField] private bool killable = false;
    [SerializeField] Image healthBar;
    Enemy enemy;
    float fillAmount = 1.0f;

    public int pointsGiven;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindObjectOfType<Enemy>();
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
        fillAmount = currentHealth / MAX_HEALTH;
        healthBar.fillAmount = fillAmount;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
       // print("test collision " + col.gameObject.tag);
        if(col.gameObject.tag == "Bullet")
        {
            print("bullet collision");
            if(killable)
            {
                UpdateHealth(-Player.bulletDamage);
                Player.bulletDamage = 1;
                if (GetHealth() <= 0)
                {
                    ScoreManager.scoreValue += pointsGiven;
                    // Destroy(gameObject);
                    enemy.UpdateHealth(enemy.MAX_HEALTH);
                    enemy.AddToRespawnList();
                    enemy.SetState(Enemy.State.Idle);
                    gameObject.SetActive(false);
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
                UpdateHealth(-Player.bulletDamage);
                Player.bulletDamage = 1;
                if (GetHealth() <= 0)
                {
                    // Destroy(gameObject);
                    enemy.UpdateHealth(enemy.MAX_HEALTH);
                    enemy.AddToRespawnList();
                    enemy.SetState(Enemy.State.Idle);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
