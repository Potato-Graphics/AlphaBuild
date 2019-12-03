using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    // Start is called before the first frame update
    float moveSpeed = 10f;

    Rigidbody rb;

    public GameObject implosionPrefab;
    public GameObject sparksPrefab;

    public AudioSource source;
    public AudioClip explosionSound;
    public AudioClip bomb;

    Player target;
    Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindObjectOfType<Player>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag.Contains("Obstacles"))
        {
            Instantiate(implosionPrefab, gameObject.transform.position, Quaternion.identity);
            source.PlayOneShot(explosionSound);
            Destroy(gameObject);
        }

        else if (col.gameObject.tag == "Bullet")
        {
            Instantiate(sparksPrefab, gameObject.transform.position, Quaternion.identity);
            source.PlayOneShot(bomb);
            Destroy(gameObject);
        }
    }
}
