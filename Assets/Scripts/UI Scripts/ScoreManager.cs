using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private bool hasWon = false;

    public static int scoreValue;

    public int ScoreValue
    {
        get { return scoreValue; }
        set { scoreValue = Mathf.Clamp(value, 0, 2147000000); }
    }

    Text score;

    public EndMenu endMenu;

    private float timeSecond = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "" + ScoreValue;
        if (Time.time >= timeSecond)
        {
            ScoreValue -= ScoreValue / 10;
            timeSecond += 3;
        }
    }

    public void OnWin()
    {
        hasWon = true;
        PlayerPrefs.SetFloat("Highscore", ScoreValue);
        endMenu.ToggleEndMenu(ScoreValue);
    }
  
}
