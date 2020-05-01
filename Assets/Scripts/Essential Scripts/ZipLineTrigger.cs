using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZipLineTrigger : MonoBehaviour
{
    Player player;
    Animator anim;
    
    ZiplineHandler ziplineHandler;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        ziplineHandler = GameObject.FindObjectOfType<ZiplineHandler>();
        anim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && !player.ridingZipline)
        {
            player.ridingZipline = true;
            anim.SetBool("IsGrounded", true);
        }
   

    }
}
