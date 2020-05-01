using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{

    public void Update()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
        print ("Quitting game...");
        
    }
}
