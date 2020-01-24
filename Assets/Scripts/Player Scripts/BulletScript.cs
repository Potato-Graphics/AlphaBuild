using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damageToGive;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Castle")
        {
            other.gameObject.GetComponent<CastleDamageScript>().DamageCastle(damageToGive);
            print("CastleDamaged");
            Destroy(gameObject);
        }
    }
}
