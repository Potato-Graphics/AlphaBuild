﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    #region public variables
    public GameObject OptionsScreen;
    public GameObject StartScreen;

    #endregion

    #region Private Variables
    private int curScreenID = 0;
    private Animator curAnim;
    #endregion


    #region UI Actions
    public void SwitchScreens(int aID)
    {
        curScreenID = aID;
        if(!curAnim)
        {
            curAnim = GetComponent<Animator>();
        }
        if (curAnim)
        {
            curAnim.SetBool("IsActive", false);
        }    
    }
    #endregion
    public void ResetMenu()
    {
        if (!curAnim)
        {
            curAnim.SetBool("IsActive", true);
        }
    }



    public void AnimSwitch()
    {
        switch (curScreenID)
        {
            case 0:
                if (OptionsScreen)
                {
                    OptionsScreen.SetActive(true);
                }
                break;

            case 1:
                if (StartScreen)
                {
                    StartScreen.SetActive(true);
                }
                break;

        }
    }



}