using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.LogWarning(col.gameObject.tag);
        if(col.gameObject.tag == "Player")
        {
            Player.spawnLocation = transform.position;
            Destroy(gameObject);
        }
    }
}
