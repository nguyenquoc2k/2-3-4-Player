using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject menuPause;
    [SerializeField] private ShowResultGameDemo showResultGameDemo;
    [SerializeField] private ShowResult showResult;

    private void Awake()
    {
        menuPause.SetActive(false);
    }

    public void OnClickPause()
    {
        Time.timeScale = 0;
        menuPause.SetActive(true);
    }

    public void OnClickMenu()
    {
        if (showResultGameDemo != null)
            showResultGameDemo.OnClickMenu();
        if (showResult != null) showResult.OnClickMenu();
    }

    public void OnClickResume()
    {
        Time.timeScale = 1;
        
        menuPause.SetActive(false);
    }

    public void OnClickPlayAgain()
    {
        Time.timeScale = 1;
        menuPause.SetActive(false);
        if (showResultGameDemo != null)
            showResultGameDemo.OnClickPlayAgain();
        if (showResult != null) showResult.OnClickPlayAgain();
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}