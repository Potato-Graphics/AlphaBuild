using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameOver : MonoBehaviour
{


    public void Retry()
    {
        Scene m_Scene = SceneManager.GetActiveScene();
        string sceneName = m_Scene.name;
        SceneManager.LoadScene(sceneName);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);

    }




    public void Quit()
    {

        Application.Quit();



    }





}
