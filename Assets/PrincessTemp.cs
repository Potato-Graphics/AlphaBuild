using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessTemp : MonoBehaviour
{
    [SerializeField]
    GameObject Crystal;

    float fireRate;
    float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1f;
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
            Instantiate(Crystal, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}

