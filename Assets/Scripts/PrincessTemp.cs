using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessTemp : MonoBehaviour
{
    [SerializeField]
    public GameObject Crystal;

    public float fireRate;
    public float nextFire;
    

    [SerializeField] Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 2f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfTimeToFire();
    }
    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            Instantiate<GameObject>(Crystal, firePoint.position, firePoint.rotation);
            nextFire = Time.time + fireRate;
        }
    }
}

