using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damageToGive;
    [SerializeField] public static float xDirection = 0.0f;
    [SerializeField] public static float yDirection = 0.0f;
    Weapon weaponScript;

    void Start()
    {
        weaponScript = GameObject.FindObjectOfType<Weapon>();
    }
    void OnCollisionEnter(Collision other)
    {
        
    }

    Vector2 VectorFromAngle(float theta)
    {
        return new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
    }

    void Update ()
    {
        Vector2 position = transform.position;
        position.x += xDirection;
        position.y += yDirection;
        transform.position = position;
    }
}
