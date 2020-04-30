using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public Text scoreText;

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        print("pressed menu button");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
