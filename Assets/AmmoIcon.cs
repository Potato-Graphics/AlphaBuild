using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class AmmoIcon : MonoBehaviour
{
    public Slider SuperAttack;
    public bool isFiring;
    public int superSoakerAmmo;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        SuperAttack.Slider = superSoakerAmmo.ToString();
        if (Input.GetMouseButtonDown(0) && !isFiring && superSoakerAmmo > 0)
        {
            isFiring = true;
            superSoakerAmmo--;
            isFiring = false;
        }
    }
}
