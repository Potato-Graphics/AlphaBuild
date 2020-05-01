using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAppear : MonoBehaviour
{
    public AudioSource endtrack;
    public GameObject endMenu;

    public GameObject Image;

    public GameObject healthUI;

    public GameObject scoreAmount;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.LogError("wergwerp");
            healthUI.SetActive(false);

            scoreAmount.SetActive(true);
            endMenu.SetActive(true);
            Image.SetActive(true);
            Time.timeScale = 0;
            endtrack.Play();
        
        }
    }
}
