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

    public GameObject AudioOST;

    public GameObject scoreAmount;
    private void Start()
    {
        endtrack.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.LogError("wergwerp");
            healthUI.SetActive(false);

            scoreAmount.SetActive(true);
            endMenu.SetActive(true);
            AudioOST.gameObject.SetActive(false);
            Image.SetActive(true);
            Time.timeScale = 0;
            endtrack.gameObject.SetActive(true);
            endtrack.Play();
        
        }
    }
}
