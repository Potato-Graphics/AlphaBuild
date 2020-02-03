using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    [Serializefield] private SpecialBar specialBar;

    
    
    // Start is called before the first frame update
    private void Start()
    {
        specialBar.Setsize(.4f);
    }
   
}
