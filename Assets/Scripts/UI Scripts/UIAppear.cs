using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAppear : MonoBehaviour
{
    public GameObject endMenu;

    public GameObject Image;

    public GameObject healthUI;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.LogError("wergwerp");
            healthUI.SetActive(false);

            
            endMenu.SetActive(true);
            Image.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
