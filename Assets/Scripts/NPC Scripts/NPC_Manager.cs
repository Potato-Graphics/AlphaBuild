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
    public GameObject coin;
    float fillAmount = 1.0f;

    public int pointsGiven;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
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
        if (col.gameObject.tag == "Bullet")
        {
            print("bullet collision" );
            if (enemy.GetEnemyType() == Enemy.EnemyType.HelicopterSeed)
            {
                transform.position = new Vector3(1000, 1000, 1000);
                StartCoroutine(RespawnHelicopter());
            }
            if (killable)
            {
                UpdateHealth(-Player.bulletDamage);
                Player.bulletDamage = 1;
                if (GetHealth() <= 0)
                {
                    ScoreManager.scoreValue += pointsGiven;
                    enemy.UpdateHealth(enemy.MAX_HEALTH);
                    enemy.SetState(Enemy.State.Dead);
                    if (enemy.GetEnemyType() != Enemy.EnemyType.HelicopterSeed)
                    {
                        Instantiate(coin, transform.position, transform.rotation);
                    }
                }
            }
        }
    }

    IEnumerator RespawnHelicopter()
    {
        yield return new WaitForSeconds(1);
        transform.position = enemy.startPosition;
    }
    void OnCollisionEnter(Collision col)
    {
        print("this is the collision" + col.gameObject.tag);
        if (col.gameObject.tag == "Bullet")
        {
            print("bullet collision");
            Debug.LogError(col.gameObject.GetComponent<Enemy>().GetState());
            if (enemy.GetEnemyType() == Enemy.EnemyType.HelicopterSeed)
            {
                Debug.LogError("w");
                transform.position = enemy.startPosition;
            }
            if (killable)
            {
                UpdateHealth(-Player.bulletDamage);
                Player.bulletDamage = 1;
                if (GetHealth() <= 0)
                {
                    enemy.UpdateHealth(enemy.MAX_HEALTH);
                    enemy.SetState(Enemy.State.Dead);
                }
            }
        }
    }
}
