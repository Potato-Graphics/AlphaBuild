using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float fireRate = 0;
    public float Damage = 10;
    public LayerMask notToHit;
    public Rigidbody2D bulletPrefab;
    [SerializeField] private float speed = 20;

    private bool delay = false;
    float timeToFire = 0;
    [SerializeField]Transform firePoint;

    void Awake ()
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
        Vector3 firePointPosition = new Vector3(firePoint.position.x, firePoint.position.y); // Stores the firepoint as a Vector2.
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, (dir - firePointPosition) * 100, notToHit); //Makes a raycast in the direction of the mouse, stops at 100.
        Debug.DrawLine(firePointPosition, (dir - firePointPosition) * 100, Color.red); //Draws the Raycast.
        Rigidbody2D test = Instantiate<Rigidbody2D>(bulletPrefab, firePoint.position, firePoint.rotation);

        test.velocity = transform.TransformDirection(dir * (speed * Time.deltaTime));
        delay = true;
        StartCoroutine(ShootDelay());
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(.1f);
        delay = false;
    }
}