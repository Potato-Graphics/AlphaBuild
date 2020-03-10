using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZipLineTrigger : MonoBehaviour
{
    Player player;
    ZiplineHandler ziplineHandler;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        ziplineHandler = GameObject.FindObjectOfType<ZiplineHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        print("zipline trigger tag is " + col.gameObject.tag);
        Debug.LogError("HHDFOIOOFDS");
        if(col.gameObject.tag == "Player" && !player.ridingZipline)
        {
            player.ridingZipline = true;
        }
    }
}
