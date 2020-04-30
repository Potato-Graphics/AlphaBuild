using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlane : MonoBehaviour
{
    public GameObject plane;
    public Transform planeTransform;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Instantiate(plane, planeTransform.position, planeTransform.rotation);
        }

    }
}
