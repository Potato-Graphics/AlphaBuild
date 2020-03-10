using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class WeaponController : MonoBehaviour
{
    public Rigidbody2D bulletHorizontal;
    public float xFire;
    public float yFire;
    public Transform firePoint;
    public float shotDelay;

    // Update is called once per frame
    void Start()
    {
        xFire = Input.GetAxisRaw("HShooting");
        yFire = Input.GetAxisRaw("VShooting");

        //Horizontal
        if(xFire > .2)
        {
            xFire = 5;
        }

        if (xFire < -.2)
        {
            xFire = -5;
        }

        //Vertical
        if (yFire > .2)
        {
            yFire = 5;
        }

        if (yFire < -.2)
        {
            yFire = -5;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector3 (xFire, yFire, 0);
    }

    void Update()
    {
        xFire = Input.GetAxisRaw("HShooting");
        yFire = Input.GetAxisRaw("VShooting");

        if (xFire > .2 || xFire < -.2)
        {
            Instantiate(firePoint, transform.position, firePoint.rotation);
        }

        if (yFire > .2 || yFire < -.2)
        {
            Instantiate(firePoint, transform.position, firePoint.rotation);
        }
    }
}
