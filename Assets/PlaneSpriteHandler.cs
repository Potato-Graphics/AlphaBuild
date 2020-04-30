using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class PlaneSpriteHandler : MonoBehaviour
{
    AIPath aiPath;
    // Start is called before the first frame update
    void Start()
    {
        aiPath = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
