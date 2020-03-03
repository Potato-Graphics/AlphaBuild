using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZiplineHandler : MonoBehaviour
{
    [SerializeField]Player player;
    [SerializeField]GameObject endPoint;
    Vector3 startPosition;
    private float journeyLength;

    private float timeStartedLerping;
    public float speed = 1.0f;
    Transform ziplinePoint;
    ZipLinePoint ziplPoint;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        startPosition = transform.position;
        timeStartedLerping = Time.time;
        journeyLength = Vector3.Distance(startPosition, endPoint.transform.position);
        
        ziplPoint = GameObject.FindObjectOfType<ZipLinePoint>();
    }



    // Update is called once per frame
    void Update()
    {
        if(player.ridingZipline)
        {
            float distCovered = (Time.time - timeStartedLerping) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, endPoint.transform.position, fractionOfJourney);
            if(fractionOfJourney >= 1)
            {
                IncrementZiplineStage();
                fractionOfJourney = 0;
                distCovered = 0;
            }
        }
    }

    public void IncrementZiplineStage()
    {
       // zipLineStage = ziplPoint.ziplinePointID;
        startPosition = ziplinePoint.position;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.LogError(col.gameObject.tag);
        if(col.gameObject.tag == "Player")
        {
            if (player.ridingZipline) return;
           // player.ridingZipline = true;
            print("ziplined" + player.ridingZipline);
        }
    }
}
