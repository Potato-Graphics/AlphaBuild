using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    private Player player;
    private Animator anim;
    bool m_BackwardsDiagUp;
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
    public Image specialBar;

    float timeToFire = 0;


    public Transform firePointDown;
    public Transform firePointUp;
    public Transform firePointHorizontal;
    public Transform firePointUpDiagonal;
    public Transform firePointDownDiagonal;

    public Transform muzzlePointUp;
    public Transform muzzlePointDown;
    public Transform muzzlePointHorizontal;
    public Transform muzzlePointUpDiag;
    public Transform muzzlePointDownDiag;

    public Rigidbody2D muzzleFlashUp;
    public Rigidbody2D muzzleFlashDown;
    public Rigidbody2D muzzleFlashHorizontal;
    public Rigidbody2D muzzleFlashUpDiag;
    public Rigidbody2D muzzleFlashDownDiag;


    void Start()
    {
        player = GetComponent<Player>();
        anim = player.GetComponent<Animator>();
        m_BackwardsDiagUp = false;
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
        float joyangle = Mathf.Atan2(Input.GetAxis("JoyStickX"), Input.GetAxis("JoyStickY")) * Mathf.Rad2Deg;
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position); // Mouse position directoin.
        Vector3 direction = Input.mousePosition;
        //Vector3 firePointPosition = new Vector3(firePoint.position.x, firePoint.position.y); // Stores the firepoint as a Vector2.



        //This finds the angle of the mouse cursor
        Vector3 v3Pos;
        float fAngle;

        // Project the mouse point into world space at
        //   at the distance of the player.
        v3Pos = Input.mousePosition;
        v3Pos.z = (transform.position.z - Camera.main.transform.position.z);
        v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);
        v3Pos = v3Pos - transform.position;
        fAngle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;
        if (fAngle < 0.0f) fAngle += 360.0f;

        // Raycast against a mathematical plane in world space
        Plane plane = new Plane(Vector3.forward, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float fDist;
        plane.Raycast(ray, out fDist);
        v3Pos = ray.GetPoint(fDist);
        v3Pos = v3Pos - transform.position;
        fAngle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;
        if (fAngle < 0.0f) fAngle += 360.0f;

        //Convert the player to Screen coordinates
        v3Pos = Camera.main.WorldToScreenPoint(transform.position);
        v3Pos = Input.mousePosition - v3Pos;
        fAngle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;
        if (fAngle < 0.0f) fAngle += 360.0f;
        //end of angle finding



        if (!player.usingController)
        {
            if (fAngle >= 77 && fAngle <= 110 || joyangle > 165 && joyangle < 185)
            {
                //this is up
                Rigidbody2D bullet = Instantiate(bulletUp, firePointUp.position, firePointUp.rotation);
               Rigidbody2D muzzleFlash = Instantiate(muzzleFlashUp, muzzlePointUp.position, muzzlePointUp.rotation);
                bullet.transform.localScale *= player.bulletSizeMultiplier;
                //BulletScript.yDirection = 0.1f;
                //BulletScript.xDirection = 0.0f;
                Vector2 velocityChange = new Vector2(0.0f, 0.3f);
                bullet.transform.eulerAngles = new Vector2(0, 0);
                bullet.velocity = velocityChange * (Time.deltaTime * speed);
                float angle = 90;
                //firePointUp.rotation = Quaternion.AngleAxis(55, Vector3.up);
                print("up");
                anim.SetTrigger("FireUp");
            }

            else if (fAngle >= 246 && fAngle <= 288)
            {
                //this is down
                anim.SetTrigger("FireDown");
                Rigidbody2D bullet = Instantiate(bulletDown, firePointDown.position, firePointDown.rotation);
                Rigidbody2D muzzleFlash = Instantiate(muzzleFlashDown, muzzlePointDown.position, muzzlePointDown.rotation);
                bullet.transform.localScale *= player.bulletSizeMultiplier;
                //BulletScript.yDirection = -0.1f;
                // BulletScript.xDirection = 0.0f;
                Vector2 velocityChange = new Vector2(0, -0.1f);
                bullet.transform.eulerAngles = new Vector2(0, 0);
                print("down");
                bullet.velocity = velocityChange * (Time.deltaTime * speed);

            }
            else if (fAngle < 77 && fAngle >= 19.2)
            {
                //this is right up
                anim.SetTrigger("FireDiagUp");
                Rigidbody2D bullet = Instantiate(bulletDiagUp, firePointUpDiagonal.position, firePointUpDiagonal.localRotation);
                Rigidbody2D muzzleFlash = Instantiate(muzzleFlashUpDiag, muzzlePointUpDiag.position, muzzlePointUpDiag.rotation);
                bullet.transform.localScale *= player.bulletSizeMultiplier;
                //BulletScript.xDirection = 0.1f;
                // BulletScript.yDirection = 0.1f;
                Vector2 velocityChange = new Vector2(0.1f, 0.1f);
                bullet.transform.eulerAngles = new Vector2(0, 0);
                print("right up");
                bullet.velocity = velocityChange * (Time.deltaTime * speed);

            }
            else if (fAngle < 19.2 && fAngle >= 0 || fAngle < 360 && fAngle > 350)
            {
                //this is right
                anim.SetTrigger("FireHorizontal");
                Rigidbody2D bullet = Instantiate(bulletHorizontal, firePointHorizontal.position, firePointHorizontal.localRotation);
                Rigidbody2D muzzleFlash = Instantiate(muzzleFlashHorizontal, muzzlePointHorizontal.position, muzzlePointHorizontal.rotation);
                bullet.transform.localScale *= player.bulletSizeMultiplier;
                // BulletScript.xDirection = 0.1f;
                // BulletScript.yDirection = 0.0f;
                Vector2 velocityChange = new Vector2(0.1f, 0);
                bullet.transform.eulerAngles = new Vector2(0, 0);
                print("right");
                bullet.velocity = velocityChange * (Time.deltaTime * speed);

            }
            else if (fAngle <= 350 && fAngle > 288)
            {
                //this is right down
                anim.SetTrigger("FireDiagDown");
                Rigidbody2D bullet = Instantiate(bulletDiagDown, firePointDownDiagonal.position, firePointDownDiagonal.localRotation);
                Rigidbody2D muzzleFlash = Instantiate(muzzleFlashDownDiag, muzzlePointDownDiag.position, muzzlePointDownDiag.rotation);
                bullet.transform.localScale *= player.bulletSizeMultiplier;
                //BulletScript.xDirection = 0.1f;
                //BulletScript.yDirection = -0.1f;
                Vector2 velocityChange = new Vector2(0.1f, -0.1f);
                bullet.transform.eulerAngles = new Vector2(0, 0);
                print("right down");
                bullet.velocity = velocityChange * (Time.deltaTime * speed);

            }
            else if (fAngle > 110 && fAngle <= 165)
            {
                //this is left up
                anim.SetTrigger("FireDiagUp");
                Rigidbody2D bullet = Instantiate(bulletDiagUp, firePointUpDiagonal.position, firePointUpDiagonal.localRotation);
                Rigidbody2D muzzleFlash = Instantiate(muzzleFlashUpDiag, muzzlePointUpDiag.position, muzzlePointUpDiag.rotation);
                bullet.transform.localScale *= player.bulletSizeMultiplier;
                //BulletScript.xDirection = -0.1f;
                // BulletScript.yDirection = 0.1f;
                Vector2 velocityChange = new Vector2(-0.1f, 0.1f);
                bullet.transform.eulerAngles = new Vector2(0, -180);
                muzzleFlash.transform.eulerAngles = new Vector2(0, -180);
                muzzleFlash.transform.eulerAngles = new Vector2(0, -180);
                print("left up");
                bullet.velocity = velocityChange * (Time.deltaTime * speed);

            }
            else if (fAngle > 165 && fAngle <= 190)
            {
                //this is left
                anim.SetTrigger("FireHorizontal");
                Rigidbody2D bullet = Instantiate(bulletHorizontal, firePointHorizontal.position, firePointHorizontal.localRotation);
                Rigidbody2D muzzleFlash = Instantiate(muzzleFlashHorizontal, muzzlePointHorizontal.position, muzzlePointHorizontal.rotation);
                bullet.transform.localScale *= player.bulletSizeMultiplier;
                //BulletScript.xDirection = -0.1f;
                // BulletScript.yDirection = 0.0f;
                Vector2 velocityChange = new Vector2(-0.1f, 0);
                bullet.transform.eulerAngles = new Vector2(0, -180);
                muzzleFlash.transform.eulerAngles = new Vector2(0, -180);
                muzzleFlash.transform.eulerAngles = new Vector2(0, -180);
                print("left");
                bullet.velocity = velocityChange * (Time.deltaTime * speed);
            }
            else if (fAngle > 190 && fAngle < 246)
            {
                //this is left down
                anim.SetTrigger("FireDiagDown");
                Rigidbody2D bullet = Instantiate(bulletDiagDown, firePointDownDiagonal.position, firePointDownDiagonal.localRotation);
                Rigidbody2D muzzleFlash = Instantiate(muzzleFlashDownDiag, muzzlePointDownDiag.position, muzzlePointDownDiag.rotation);
                bullet.transform.localScale *= player.bulletSizeMultiplier;
                // BulletScript.xDirection = -0.1f;
                //  BulletScript.yDirection = -0.1f;
                Vector2 velocityChange = new Vector2(-0.1f, -0.1f);
                print("left down");
                bullet.transform.eulerAngles = new Vector2(0, -180);
                muzzleFlash.transform.eulerAngles = new Vector2(0, -180);
                muzzleFlash.transform.eulerAngles = new Vector2(0, -180);
                bullet.velocity = velocityChange * (Time.deltaTime * speed);
            }
        }
        /*
        else if (player.usingController)
        {
            if (joyangle < 34 && joyangle > -34)
            {
                //this is up
                Rigidbody2D bullet = Instantiate(bulletUp, firePoint.position, firePoint.rotation);
                bullet.transform.localScale *= player.bulletSizeMultiplier;
                //BulletScript.yDirection = 0.1f;
                //BulletScript.xDirection = 0.0f;
                Vector2 velocityChange = new Vector2(0.0f, 0.3f);
                bullet.velocity = velocityChange * (Time.deltaTime * speed);
                float angle = 90;
                firePoint.rotation = Quaternion.AngleAxis(55, Vector3.up);
                print("up");
                anim.SetTrigger("FireUp");
            }

            else if (joyangle > 125 && joyangle < 180 || joyangle < -161 && joyangle > -180)
            {
                //this is down
                anim.SetTrigger("FireDown");
                Rigidbody2D bullet = Instantiate(bulletDown, firePoint.position, firePoint.rotation);
                bullet.transform.localScale *= player.bulletSizeMultiplier;
                //BulletScript.yDirection = -0.1f;
                // BulletScript.xDirection = 0.0f;
                Vector2 velocityChange = new Vector2(0, -0.1f);
                print("down");
                bullet.velocity = velocityChange * (Time.deltaTime * speed);

            }
            else if (joyangle > 34 && joyangle < 65)
            {
                //this is right up
                anim.SetTrigger("FireDiagUp");
                Rigidbody2D bullet = Instantiate(bulletDiagUp, firePoint.position, firePoint.localRotation);
                bullet.transform.localScale *= player.bulletSizeMultiplier;
                //BulletScript.xDirection = 0.1f;
                // BulletScript.yDirection = 0.1f;
                Vector2 velocityChange = new Vector2(0.1f, 0.1f);
                print("right up");
                bullet.velocity = velocityChange * (Time.deltaTime * speed);

            }
            else if (joyangle > 65 && joyangle < 90)
            {
                //this is right
                anim.SetTrigger("FireHorizontal");
                Rigidbody2D bullet = Instantiate(bulletHorizontal, firePoint.position, firePoint.localRotation);
                bullet.transform.localScale *= player.bulletSizeMultiplier;
                // BulletScript.xDirection = 0.1f;
                // BulletScript.yDirection = 0.0f;
                Vector2 velocityChange = new Vector2(0.1f, 0);
                print("right");
                bullet.velocity = velocityChange * (Time.deltaTime * speed);

            }
            else if (joyangle > 90 && joyangle < 125)
            {
                //this is right down
                anim.SetTrigger("FireDiagDown");
                Rigidbody2D bullet = Instantiate(bulletDiagDown, firePoint.position, firePoint.localRotation);
                bullet.transform.localScale *= player.bulletSizeMultiplier;
                //BulletScript.xDirection = 0.1f;
                //BulletScript.yDirection = -0.1f;
                Vector2 velocityChange = new Vector2(0.1f, -0.1f);
                print("right down");
                bullet.velocity = velocityChange * (Time.deltaTime * speed);

            }
            else if (joyangle < -34 && joyangle > -65)
            {
                //this is left up
                anim.SetTrigger("FireDiagUp");
                Rigidbody2D bullet = Instantiate(bulletDiagUp, firePoint.position, firePoint.localRotation);
                bullet.transform.localScale *= player.bulletSizeMultiplier;
                //BulletScript.xDirection = -0.1f;
                // BulletScript.yDirection = 0.1f;
                Vector2 velocityChange = new Vector2(-0.1f, 0.1f);
                bullet.transform.eulerAngles = new Vector2(0, -180);
                print("left up");
                bullet.velocity = velocityChange * (Time.deltaTime * speed);

            }
            else if (joyangle < -65 && joyangle > -90)
            {
                //this is left
                anim.SetTrigger("FireHorizontal");
                Rigidbody2D bullet = Instantiate(bulletHorizontal, firePoint.position, firePoint.localRotation);
                bullet.transform.localScale *= player.bulletSizeMultiplier;
                //BulletScript.xDirection = -0.1f;
                // BulletScript.yDirection = 0.0f;
                Vector2 velocityChange = new Vector2(-0.1f, 0);
                bullet.transform.eulerAngles = new Vector2(0, -180);
                print("left");
                bullet.velocity = velocityChange * (Time.deltaTime * speed);
            }
            else if (joyangle < -90 && joyangle > -125)
            {
                //this is left down
                anim.SetTrigger("FireDiagDown");
                Rigidbody2D bullet = Instantiate(bulletDiagDown, firePoint.position, firePoint.localRotation);
                bullet.transform.localScale *= player.bulletSizeMultiplier;
                // BulletScript.xDirection = -0.1f;
                //  BulletScript.yDirection = -0.1f;
                Vector2 velocityChange = new Vector2(-0.1f, -0.1f);
                print("left down");
                bullet.transform.eulerAngles = new Vector2(0, -180);
                bullet.velocity = velocityChange * (Time.deltaTime * speed);
            }
        }*/

        delay = true;
        StartCoroutine(ShootDelay());
        specialBar.fillAmount = 0;
        player.bulletSizeMultiplier = 1;
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(.1f);
        delay = false;
    }
}