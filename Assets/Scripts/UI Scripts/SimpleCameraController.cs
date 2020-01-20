using UnityEngine;

namespace UnityTemplateProjects
{
    public class SimpleCameraController : MonoBehaviour
    {
        Transform playerObject;
        [SerializeField]Vector3 playerPosition;
        [SerializeField] float targetPositionX;
        [SerializeField] float playerPositionX;
        [SerializeField] Vector3 targetLocation;
        float distance;

        void Start()
        {
            playerObject = GameObject.FindGameObjectWithTag("Player").transform;
            playerPosition = playerObject.transform.position;
        }

        void Update()
        {
            playerPosition = playerObject.transform.position;
            playerPositionX = playerPosition.x;
            targetPositionX = playerPositionX - 1;
            
            targetLocation = new Vector3(targetPositionX, 8, -23.2f);
           

            transform.position = targetLocation;


        }
    }
}