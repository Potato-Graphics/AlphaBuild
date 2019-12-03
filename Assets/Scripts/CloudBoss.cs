using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBoss : MonoBehaviour
{

    [SerializeField]private Player thePlayer;
    public float moveSpeed;
    public float playerRange;

    // Gets the player game object.
    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
    }

    // Follows the player's position.
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
    }
}
