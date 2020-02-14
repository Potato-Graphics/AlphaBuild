using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int scoreValue;
    Text score;

    private float timeSecond = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + scoreValue;
        if (Time.time >= timeSecond)
        {
            scoreValue -= 10;
            timeSecond += 3;
        }
    }
  
}
