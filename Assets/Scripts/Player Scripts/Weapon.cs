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
    public Rigidbody2D bulletUp;
    public Rigidbody2D bulletDown;
    public Rigidbody2D bulletHorizontal;
    public Rigidbody2D bulletDiagDown;
    public Rigidbody2D bulletDiagUp;
    public Vector3 position;
    [SerializeField] float speed = 25;

    float timeToFire = 0;


    public Transform firePoint;
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
        if (direction.y > 237 && direction.y < 327 && direction.x > 337 && direction.x < 360)
        {
            //this is up
            Rigidbody2D bullet = Instantiate(bulletUp, firePoint.position, firePoint.rotation);
            //BulletScript.yDirection = 0.1f;
            //BulletScript.xDirection = 0.0f;
            Vector2 velocityChange = new Vector2(0.0f, 0.1f);
            bullet.velocity = velocityChange * (Time.deltaTime * speed);
            print("up");
            anim.SetTrigger("FireUp");
        }

        else if (direction.y > 112 && direction.y < 225 && direction.x > 337 && direction.x < 360)
        {
            //this is down
            anim.SetTrigger("FireDown");
            Rigidbody2D bullet = Instantiate(bulletDown, firePoint.position, firePoint.rotation);
            //BulletScript.yDirection = -0.1f;
            // BulletScript.xDirection = 0.0f;
            Vector2 velocityChange = new Vector2(0, -0.1f);
            print("down");
            bullet.velocity = velocityChange * (Time.deltaTime * speed);

        }
        else if (direction.x > 638 && direction.x < 1200 && direction.y < 600 && direction.y > 316)
        {
            //this is right up
            anim.SetTrigger("FireDiagUp");
            Rigidbody2D bullet = Instantiate(bulletDiagUp, firePoint.position, firePoint.localRotation);
            //BulletScript.xDirection = 0.1f;
            // BulletScript.yDirection = 0.1f;
            Vector2 velocityChange = new Vector2(0.1f, 0.1f);
            print("rignht up");
            bullet.velocity = velocityChange * (Time.deltaTime * speed);

        }
        else if (direction.x > 638 && direction.x < 1200 && direction.y > 266 && direction.y < 316)
        {
            //this is right
            anim.SetTrigger("FireHorizontal");
            Rigidbody2D bullet = Instantiate(bulletHorizontal, firePoint.position, firePoint.localRotation);
            // BulletScript.xDirection = 0.1f;
            // BulletScript.yDirection = 0.0f;
            Vector2 velocityChange = new Vector2(0.1f, 0);
            print("right");
            bullet.velocity = velocityChange * (Time.deltaTime * speed);

        }
        else if (direction.x > 361 && direction.x < 657 && direction.y < 175 && direction.y > 27)
        {
            //this is right down
            anim.SetTrigger("FireDiagDown");
            Rigidbody2D bullet = Instantiate(bulletDiagDown, firePoint.position, firePoint.localRotation);
            //BulletScript.xDirection = 0.1f;
            //BulletScript.yDirection = -0.1f;
            Vector2 velocityChange = new Vector2(0.1f, -0.1f);
            print("right down");
            bullet.velocity = velocityChange * (Time.deltaTime * speed);

        }
        else if (direction.x > 166 && direction.x < 339 && direction.y < 377 && direction.y > 228)
        {
            //this is left up
            anim.SetTrigger("FireDiagUp");
            Rigidbody2D bullet = Instantiate(bulletDiagUp, firePoint.position, firePoint.localRotation);
            //BulletScript.xDirection = -0.1f;
            // BulletScript.yDirection = 0.1f;
            Vector2 velocityChange = new Vector2(-0.1f, 0.1f);
            bullet.transform.eulerAngles = new Vector2(0, -180);
            print("left up");
            bullet.velocity = velocityChange * (Time.deltaTime * speed);

        }
        else if (direction.x > 15 && direction.x < 328 && direction.y > 189 && direction.y < 204)
        {
            //this is left
            anim.SetTrigger("FireHorizontal");
            Rigidbody2D bullet = Instantiate(bulletHorizontal, firePoint.position, firePoint.localRotation);
            //BulletScript.xDirection = -0.1f;
            // BulletScript.yDirection = 0.0f;
            Vector2 velocityChange = new Vector2(-0.1f, 0);
            bullet.transform.eulerAngles = new Vector2(0, -180);
            print("left");
            bullet.velocity = velocityChange * (Time.deltaTime * speed);
        }
        else if (direction.x > 11 && direction.x < 322 && direction.y < 173 && direction.y > 29)
        {
            //this is left down
            anim.SetTrigger("FireDiagDown");
            Rigidbody2D bullet = Instantiate(bulletDiagDown, firePoint.position, firePoint.localRotation);
            // BulletScript.xDirection = -0.1f;
            //  BulletScript.yDirection = -0.1f;
            Vector2 velocityChange = new Vector2(-0.1f, -0.1f);
            print("left down");
            bullet.transform.eulerAngles = new Vector2(0, -180);
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