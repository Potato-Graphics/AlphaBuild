using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CanvasController : MonoBehaviour
{
    #region Public Variables
    public bool isTimedScreen = false;
    public float screenTime = 3f;
    public GameObject targetTimedScreen;
    #endregion


    #region Private Variables
    private float startTime = 0f;
    #endregion


    #region Main Methods




    // Start is called before the first frame update
    void Start()
    {
        SetupCanvas ();
    }

    void OnEnable()
    {
        SetupCanvas ();
    }
    // Update is called once per frame
    void Update()
    {
        if (isTimedScreen) //keeps splash screen active to screenTime float value
        {
            while (Time.time < startTime + screenTime)
            {
                return;
            }
            if (targetTimedScreen) //Turns off Splash screen, Activates Start screen
            {
                gameObject.SetActive(false);
                targetTimedScreen.SetActive(true);
            }
        }
    }

    #endregion

    #region Utility Methods
    void SetupCanvas()
    {
        startTime = Time.time;
    }
    #endregion
}
