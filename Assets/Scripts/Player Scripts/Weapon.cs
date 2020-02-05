using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float fireRate = 0;
    public LayerMask notToHit;
    public Rigidbody2D bulletPrefab;
    [SerializeField] Player player;
    [SerializeField] public float speed = 20;
    public Rigidbody2D bulletRB;
    public Transform firePoint;
    public float angle;

    private bool delay = false;
    float timeToFire = 0;


    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
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
        Vector3 direction = dir - firePoint.position;
        direction = firePoint.InverseTransformDirection(direction).normalized;
        angle = Vector3.Angle(firePointPosition, ((dir - firePointPosition) * 100));
        Debug.LogWarning(angle);
        Debug.DrawLine(firePointPosition, (dir - firePointPosition) * 100, Color.red); //Draws the Raycast.
        bulletRB = Instantiate<Rigidbody2D>(bulletPrefab, firePoint.position, firePoint.rotation);
        //test.velocity = transform.TransformDirection(dir * (speed * Time.deltaTime));
        delay = true;
        StartCoroutine(ShootDelay());
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(.1f);
        delay = false;
    }
}