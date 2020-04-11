using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float gameTimer = 0;
    public Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;

        string minutes = Mathf.Floor(gameTimer / 60).ToString("00");
        string seconds = (gameTimer % 60).ToString("00");

        timerText.text = (string.Format("{0}:{1}", minutes, seconds));
    }
}
