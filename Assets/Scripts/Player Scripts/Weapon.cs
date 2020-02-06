﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float fireRate = 0;
    public LayerMask notToHit;
    public Rigidbody2D bulletPrefab;
    public Transform firePoint;
    public Vector3 position;

    private bool delay = false;
    float timeToFire = 0;


    void Start()
    {
       
      
    }
    void Update()
    {
        //If the button is held down, starts auto firing until button is released.
        if (Input.GetAxis("Fire1") > 0.0f && !delay)
        {
            timeToFire = Time.deltaTime + 1 / fireRate;
            Shooting();
        }
    }

    void Shooting()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position); // Mouse position directoin.
        Vector3 direction = Input.mousePosition;
        Vector3 firePointPosition = new Vector3(firePoint.position.x, firePoint.position.y); // Stores the firepoint as a Vector2.
        Debug.DrawLine(firePointPosition, (dir - firePointPosition) * 100, Color.red); //Draws the Raycast.
        Debug.LogError(direction);
        if (direction.y > 350 && direction.y < 600 && direction.x > 450 && direction.x < 542)
        {
            Rigidbody2D bullet = Instantiate<Rigidbody2D>(bulletPrefab, firePoint.position, firePoint.rotation);
            BulletScript.yDirection = 0.1f;
            BulletScript.xDirection = 0.0f;
            print("up");
        }
        else if (direction.y > 0 && direction.y < 240 && direction.x > 450 && direction.x < 542)
        {
            Rigidbody2D bullet = Instantiate<Rigidbody2D>(bulletPrefab, firePoint.position, firePoint.rotation);
            BulletScript.yDirection = -0.1f;
            BulletScript.xDirection = 0.0f;
            print("down");
        }
        else if (direction.x > 600 && direction.x < 1000 && direction.y < 600 && direction.y > 370)
        {
            Rigidbody2D bullet = Instantiate<Rigidbody2D>(bulletPrefab, firePoint.position, firePoint.rotation);
            BulletScript.xDirection = 0.1f;
            BulletScript.yDirection = 0.1f;
            print("RightUpdiagonal");
        }
        else if (direction.x > 525 && direction.x < 1000 && direction.y > 270 && direction.y < 370)
        {
            Rigidbody2D bullet = Instantiate<Rigidbody2D>(bulletPrefab, firePoint.position, firePoint.rotation);
            BulletScript.xDirection = 0.1f;
            BulletScript.yDirection = 0.0f;
            print("Rightforward");
        }
        else if (direction.x > 450 && direction.x < 1000 && direction.y < 270 && direction.y > 0)
        {
            print("RightDownDiagonal");
            Rigidbody2D bullet = Instantiate<Rigidbody2D>(bulletPrefab, firePoint.position, firePoint.rotation);
            BulletScript.xDirection = 0.1f;
            BulletScript.yDirection = -0.1f;
        }
        else if (direction.x > 0 && direction.x < 450 && direction.y < 600 && direction.y > 370)
        {
            Rigidbody2D bullet = Instantiate<Rigidbody2D>(bulletPrefab, firePoint.position, firePoint.rotation);
            BulletScript.xDirection = -0.1f;
            BulletScript.yDirection = 0.1f;
            print("LeftUpdiagonal");
        }
        else if (direction.x > 0 && direction.x < 525 && direction.y > 270 && direction.y < 370)
        {
            Rigidbody2D bullet = Instantiate<Rigidbody2D>(bulletPrefab, firePoint.position, firePoint.rotation);
            BulletScript.xDirection = -0.1f;
            BulletScript.yDirection = 0.0f;
            print("Leftforward");
        }
        else if (direction.x > 0 && direction.x < 450 && direction.y < 270 && direction.y > 0)
        {
            print("LeftDownDiagonal");
            Rigidbody2D bullet = Instantiate<Rigidbody2D>(bulletPrefab, firePoint.position, firePoint.rotation);
            BulletScript.xDirection = -0.1f;
            BulletScript.yDirection = -0.1f;
        }

        delay = true;
        StartCoroutine(ShootDelay());
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(.1f);
        delay = false;
    }
}