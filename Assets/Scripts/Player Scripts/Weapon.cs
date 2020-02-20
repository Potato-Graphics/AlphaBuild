using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Player player;
    private Animator anim;

    private bool delay = false;

    public float fireRate = 0;
    public LayerMask notToHit;
    public Rigidbody2D bulletPrefab;
    public Transform firePoint;
    public Vector3 position;
    [SerializeField] float speed = 25;

    float timeToFire = 0;


    void Start()
    {
        player = GetComponent<Player>();
        anim = player.GetComponent<Animator>();

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

    //HELLO EARTHLING. BELOW IS THE ANIMATION CALL FOR THE SHOOTING SCRIPT TO BE PUT IN EACH DIRECTION. PLEASE COMMENT EACH DIRECTION WITH ITS DIRECTION
    //   anim.SetTrigger("FireUp"); anim.SetTrigger("FireDiagUp"); anim.SetTrigger("FireHorizontal"); anim.SetTrigger("FireDiagDown"); anim.SetTrigger("FireDown");


    void Shooting()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position); // Mouse position directoin.
        Vector3 direction = Input.mousePosition;
        Vector3 firePointPosition = new Vector3(firePoint.position.x, firePoint.position.y); // Stores the firepoint as a Vector2.
        Debug.DrawLine(firePointPosition, (dir - firePointPosition) * 100, Color.red); //Draws the Raycast.
        Debug.LogWarning("mouse position: " + direction);
        if (direction.y > 350 && direction.y < 600 && direction.x > 450 && direction.x < 542)
        {
            //this is up
            Rigidbody2D bullet = Instantiate<Rigidbody2D>(bulletPrefab, firePoint.position, firePoint.rotation);
            //BulletScript.yDirection = 0.1f;
            //BulletScript.xDirection = 0.0f;
            Vector2 velocityChange = new Vector2(0.0f, 0.1f);
            bullet.velocity = velocityChange * (Time.deltaTime * speed);
            anim.SetTrigger("FireUp");
        }
        else if (direction.y > 0 && direction.y < 240 && direction.x > 450 && direction.x < 542)
        {
            //this is down
            anim.SetTrigger("FireDown");
            Rigidbody2D bullet = Instantiate<Rigidbody2D>(bulletPrefab, firePoint.position, firePoint.rotation);
            //BulletScript.yDirection = -0.1f;
            // BulletScript.xDirection = 0.0f;
            Vector2 velocityChange = new Vector2(0, -0.1f);
            bullet.velocity = velocityChange * (Time.deltaTime * speed);
        }
        else if (direction.x > 600 && direction.x < 1000 && direction.y < 600 && direction.y > 370)
        {
            //this is right up
            anim.SetTrigger("FireDiagUp");
            Rigidbody2D bullet = Instantiate<Rigidbody2D>(bulletPrefab, firePoint.position, firePoint.rotation);
            //BulletScript.xDirection = 0.1f;
            // BulletScript.yDirection = 0.1f;
            Vector2 velocityChange = new Vector2(0.1f, 0.1f);
            bullet.velocity = velocityChange * (Time.deltaTime * speed);
        }
        else if (direction.x > 525 && direction.x < 1000 && direction.y > 270 && direction.y < 370)
        {
            //this is right
            anim.SetTrigger("FireHorizontal");
            Rigidbody2D bullet = Instantiate<Rigidbody2D>(bulletPrefab, firePoint.position, firePoint.rotation);
            // BulletScript.xDirection = 0.1f;
            // BulletScript.yDirection = 0.0f;
            Vector2 velocityChange = new Vector2(0.1f, 0);
            bullet.velocity = velocityChange * (Time.deltaTime * speed);
        }
        else if (direction.x > 450 && direction.x < 1000 && direction.y < 270 && direction.y > 0)
        {
            //this is right down
            anim.SetTrigger("FireDiagDown");
            Rigidbody2D bullet = Instantiate<Rigidbody2D>(bulletPrefab, firePoint.position, firePoint.rotation);
            //BulletScript.xDirection = 0.1f;
            //BulletScript.yDirection = -0.1f;
            Vector2 velocityChange = new Vector2(0.1f, -0.1f);
            bullet.velocity = velocityChange * (Time.deltaTime * speed);
        }
        else if (direction.x > 0 && direction.x < 450 && direction.y < 600 && direction.y > 370)
        {
            //this is left up
            anim.SetTrigger("FireDiagUp");
            Rigidbody2D bullet = Instantiate<Rigidbody2D>(bulletPrefab, firePoint.position, firePoint.rotation);
            //BulletScript.xDirection = -0.1f;
            // BulletScript.yDirection = 0.1f;
            Vector2 velocityChange = new Vector2(-0.1f, 0.1f);
            bullet.velocity = velocityChange * (Time.deltaTime * speed);
        }
        else if (direction.x > 0 && direction.x < 525 && direction.y > 270 && direction.y < 370)
        {
            //this is left
            anim.SetTrigger("FireHorizontal");
            Rigidbody2D bullet = Instantiate<Rigidbody2D>(bulletPrefab, firePoint.position, firePoint.rotation);
            //BulletScript.xDirection = -0.1f;
            // BulletScript.yDirection = 0.0f;
            Vector2 velocityChange = new Vector2(-0.1f, 0);
            bullet.velocity = velocityChange * (Time.deltaTime * speed);
        }
        else if (direction.x > 0 && direction.x < 450 && direction.y < 270 && direction.y > 0)
        {
            //this is left down
            anim.SetTrigger("FireDiagDown");
            Rigidbody2D bullet = Instantiate<Rigidbody2D>(bulletPrefab, firePoint.position, firePoint.rotation);
            // BulletScript.xDirection = -0.1f;
            //  BulletScript.yDirection = -0.1f;
            Vector2 velocityChange = new Vector2(-0.1f, -0.1f);
            bullet.velocity = velocityChange * (Time.deltaTime * speed);
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