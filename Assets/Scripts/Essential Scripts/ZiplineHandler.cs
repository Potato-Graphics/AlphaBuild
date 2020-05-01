using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZiplineHandler : MonoBehaviour
{
    [SerializeField]Player player;
    public float speed = 1.0f;
    public Transform targetPoint;
    public List<Transform> ziplinePoints = new List<Transform>();
    private int targetZiplinePointIndex = 0;
    private int lastZiplinePointIndex;
    public float minDistance = 0.1f;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        targetPoint = ziplinePoints[targetZiplinePointIndex];
        lastZiplinePointIndex = ziplinePoints.Count - 1;
        startPosition = transform.position;
    }

    public void ResetZipline()
    {
        Debug.LogError("test");
        player.ridingZipline = false;
        targetZiplinePointIndex = 0;
        targetPoint = ziplinePoints[targetZiplinePointIndex];
        Debug.LogError("pos: " + transform.position);
        transform.position = startPosition;
        Debug.LogError("pos: " + transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        if (player.ridingZipline)
        {
            float distance = Vector3.Distance(transform.position, targetPoint.position);
            CheckDistance(distance);
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
           
        }
    }

    public void IncrementZiplineStage()
    {
        Debug.LogError("targetZipLinePointIndex: " + targetZiplinePointIndex + "lastZiplinePointIndex " + lastZiplinePointIndex);
        if (targetZiplinePointIndex > lastZiplinePointIndex)
        {
            player.ridingZipline = false;
            return;
        }

        targetPoint = ziplinePoints[targetZiplinePointIndex];
    }

    void CheckDistance(float currentDistance)
    {
        if(currentDistance <= minDistance)
        {
            targetZiplinePointIndex++;
            IncrementZiplineStage();

        }
    }
}
