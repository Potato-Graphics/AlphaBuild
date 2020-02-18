using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int scoreValue;

    public int ScoreValue
    {
        get { return scoreValue; }
        set { scoreValue = Mathf.Clamp(value, 0, 2147000000); }
    }
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
        if (Input.GetAxis("Fire2") > 0.0f)
        {
            ScoreValue += 1000;
        }
        score.text = "Score: " + ScoreValue;
        if (Time.time >= timeSecond)
        {
            ScoreValue -= 10;
            timeSecond += 3;
        }
    }
  
}
