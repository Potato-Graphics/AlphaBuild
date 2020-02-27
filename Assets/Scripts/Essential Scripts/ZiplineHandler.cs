using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZiplineHandler : MonoBehaviour
{
    [SerializeField]Player player;
    private bool shouldLerp = false;
    public Vector2 endPosition;
    [SerializeField]GameObject endPoint;

    public float timeStartedLerping;
    public float lerpTime;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        StartLerping();
    }

    public Vector3 Lerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime = 1)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;

        float percentageComplete = timeSinceStarted / lerpTime;

        var result = Vector3.Lerp(start, end, percentageComplete);

        return result;
    }

    private void StartLerping()
    {
        timeStartedLerping = Time.time;

        shouldLerp = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(player.ridingZipline)
        {
            //Lerp(transform.position, new Vector2(832.73f, 21.22f), 0, 60);
            transform.position = Vector3.MoveTowards(transform.position, endPoint.transform.position, Time.deltaTime * 50);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            if (player.ridingZipline) return;
            player.ridingZipline = true;
            player.transform.position = new Vector2(410.58f, 11.77f);
            print("ziplined" + player.ridingZipline);
        }
    }
}
