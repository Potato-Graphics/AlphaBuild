using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene_OnCollision : MonoBehaviour
{
    [SerializeField] int sceneID;
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D col)
    {
        print("test");
        if (col.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(sceneID);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        print("test jd");
        print(col.gameObject.tag);
        if (col.gameObject.tag == "Player")
        {
            print("gfd");
            SceneManager.LoadScene(sceneID);
        }
    }
}