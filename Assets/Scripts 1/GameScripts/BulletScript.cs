using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public Vector3 Velocity;

    void Update()
    {
        //Moves the bullet.
        transform.position += Velocity * Time.deltaTime;
    }
}
