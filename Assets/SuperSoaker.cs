using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSoaker : MonoBehaviour
{

    private Transform bar; 
    
    // Start is called before the first frame update
    private void start()
    {
        bar = transform.Find("bar");
    }

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }
}
