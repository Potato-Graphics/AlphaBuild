using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float fireRate = 0;
    public LayerMask notToHit;
    public Rigidbody2D bulletPrefab;
    [SerializeField] Player player;
    Controller2D controller;
    [SerializeField] public float speed = 20;
    public Rigidbody2D bulletRB;
    public Transform firePointUp, firePointUpDiagonal,
        firePointForward, firePointDownDiagonal, firePointDown;
    public Vector3 position;

    private bool delay = false;
    float timeToFire = 0;


    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        controller = GameObject.FindObjectOfType<Controller2D>();
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
        Vector3 firePointPosition = new Vector3(firePointUpDiagonal.position.x, firePointUpDiagonal.position.y); // Stores the firepoint as a Vector2.
        Debug.DrawLine(firePointPosition, (dir - firePointPosition) * 100, Color.red); //Draws the Raycast.
        Debug.LogError(dir);
        if (dir.y > 48 && dir.y < 206 & dir.x < 20 && dir.x > -26)
        {
            bulletRB = Instantiate<Rigidbody2D>(bulletPrefab, firePointUp.position, firePointUp.rotation);
            print("up");
        }
        else if (dir.y < -55 && dir.y > -65)
        {
            bulletRB = Instantiate<Rigidbody2D>(bulletPrefab, firePointDown.position, firePointDown.rotation);
            print("down");
        }
        else if (dir.y > 55 && dir.y < 127 && dir.x > 0)
        {
            bulletRB = Instantiate<Rigidbody2D>(bulletPrefab, firePointUpDiagonal.position, firePointUpDiagonal.rotation);
            print("RightUpdiagonal");
        }
        else if (dir.y > 7 && dir.y < 65 && dir.x > 0)
        {
            bulletRB = Instantiate<Rigidbody2D>(bulletPrefab, firePointForward.position, firePointForward.rotation);
            print("Rightforward");
        }
        else if (dir.y > -55 && dir.y < 7 && dir.x > 0)
        {
            print("RightDownDiagonal");
            bulletRB = Instantiate<Rigidbody2D>(bulletPrefab, firePointDownDiagonal.position, firePointDownDiagonal.rotation);
        }
        else if (dir.y > 55 && dir.y < 127 && dir.x < 0)
        {
            bulletRB = Instantiate<Rigidbody2D>(bulletPrefab, firePointUpDiagonal.position, firePointUpDiagonal.rotation);
            print("LeftUpdiagonal");
        }
        else if (dir.y > 7 && dir.y < 65 && dir.x < 0)
        {
            bulletRB = Instantiate<Rigidbody2D>(bulletPrefab, firePointForward.position, firePointForward.rotation);
            print("Leftforward");
        }
        else if (dir.y > -55 && dir.y < 7 && dir.x < 0)
        {
            print("LeftDownDiagonal");
            bulletRB = Instantiate<Rigidbody2D>(bulletPrefab, firePointDownDiagonal.position, firePointDownDiagonal.rotation);
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