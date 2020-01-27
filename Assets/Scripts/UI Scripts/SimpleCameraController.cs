using UnityEngine;

namespace UnityTemplateProjects
{
    public class SimpleCameraController : MonoBehaviour
    {
        Transform playerObject;
        [SerializeField]Vector3 playerPosition;
        [SerializeField] float targetPositionX;
        [SerializeField] float playerPositionX;
        [SerializeField] float targetPositionY;
        [SerializeField] float playerPositionY;
        [SerializeField] Vector3 targetLocation;
        [SerializeField] float y;
        [SerializeField] float z;
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
            playerPositionY = playerPosition.y;
            targetPositionY = playerPositionY - 1;
            targetPositionX = playerPositionX - 1;
            if (targetPositionY < 0)
                targetPositionY = 0.5f;
            
            targetLocation = new Vector3(targetPositionX, targetPositionY, z);
           

            transform.position = targetLocation;


        }
    }
}