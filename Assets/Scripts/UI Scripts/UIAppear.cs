using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAppear : MonoBehaviour
{
    public GameObject endMenuCanvas;
    public GameObject endMenu;
    public GameObject menuButton;
    public GameObject quitButton;
    public GameObject scoreText;
    public GameObject healthUI;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.LogError("wergwerp");
            endMenuCanvas.SetActive(true);
            endMenu.SetActive(true);
            menuButton.SetActive(true);
            quitButton.SetActive(true);
            scoreText.SetActive(true);
            healthUI.SetActive(false);
        }
    }
}
