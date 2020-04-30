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

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        print("Returning to menu... ");
    }

    public void Quit()
    {
        Debug.Log("Quitting game... ");
        Application.Quit();
    }
}
