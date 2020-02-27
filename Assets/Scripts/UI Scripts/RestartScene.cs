using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartScene : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(0);
        }
    }
}
