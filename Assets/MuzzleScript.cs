using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleScript : MonoBehaviour
{
    IEnumerator DeleteMuzzle()
    {
        yield return new WaitForSeconds(3);
    }
    void Start()
    {
        StartCoroutine(DeleteMuzzle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
