using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damageToGive;
    [SerializeField] GameObject firePoint;
    [SerializeField] float speed = 1.0f;
    Player player;
    Weapon weapon;


    void Start()
    {
        firePoint = GameObject.FindGameObjectWithTag("firePoint");
        player = GameObject.FindObjectOfType<Player>();
        weapon = GameObject.FindObjectOfType<Weapon>();
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Castle")
        {
            other.gameObject.GetComponent<CastleDamageScript>().DamageCastle(damageToGive);
            print("CastleDamaged");
            Destroy(gameObject);
        }
    }

    Vector2 VectorFromAngle(float theta)
    {
        return new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
    }

    void Update ()
    {
        Debug.LogError(weapon.angle);
        weapon.bulletRB.velocity = VectorFromAngle(-weapon.angle);
        
    }
}
