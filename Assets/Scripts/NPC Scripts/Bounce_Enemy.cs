using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce_Enemy : MonoBehaviour
{
    Player player;
    int bounceRange;
    Vector3 targetLocation;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        bounceRange = Random.Range((int)player.transform.position.x - 5, (int)player.transform.position.x + 5);

    }
}
