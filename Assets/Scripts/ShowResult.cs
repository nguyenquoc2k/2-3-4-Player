using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowResult : MonoBehaviour
{
    public GameObject winner1, winner2, winner3, winner4, menu, bg;
    public Transform parentCoreGame;
    [SerializeField] private GameObject map1, map2, map3;
    public SelectMapGameDemo selectMapGameDemo;
    public static event Action HandleTime;

    private void Awake()
    {
        CoreGameController.ShowResult += ShowResultGame;
        BallController.HandleResetGame += ResetMap;
        menu.SetActive(false);
    }

    private void OnDestroy()
    {
        CoreGameController.ShowResult -= ShowResultGame;
        BallController.HandleResetGame -= ResetMap;
    }

    public void ShowResultGame(string nameWinner, string nameWinner2)
    {
        menu.SetActive(false);
        bg.SetActive(true);
        if (nameWinner2 == null)
        {
            if (nameWinner == "Player1")
            {
                winner1.SetActive(true);
                StartCoroutine(ShowMenu(1f));
            }

            if (nameWinner == "Player2")
            {
                winner2.SetActive(true);
                StartCoroutine(ShowMenu(1f));
            }

            if (nameWinner == "Player3")
            {
                winner3.SetActive(true);
                StartCoroutine(ShowMenu(1f));
            }

            if (nameWinner == "Player4")
            {
                winner4.SetActive(true);
                StartCoroutine(ShowMenu(1f));
            }
        }
        else if (nameWinner2 != null)
        {
            if (nameWinner == "Player1" || nameWinner == "Player2")
            {
                winner1.SetActive(true);
                winner3.SetActive(true);
                StartCoroutine(ShowMenu(1f));
            }
            else if (nameWinner == "Player3" || nameWinner == "Player4")
            {
                winner2.SetActive(true);
                winner4.SetActive(true);
                StartCoroutine(ShowMenu(1f));
            }
        }

        if (nameWinner2 == null && nameWinner == null)
        {
            StartCoroutine(ShowMenu(1f));
        }
    }

    public void OnClickMenu()
    {
        GameManager.Instances.lobbyManager.gameObject.SetActive(true);
        GameManager.Instances.BackToLobby();
    }

    public void OnClickPlayAgain()
    {
        ResetMap();
        if (ToggleGroupController.Instances != null && ToggleGroupController.Instances.gameMode3.isOn == true ||
            SelectMapGameDemo.Instances.modeTimeAndScore == true || MapModeStickBall.Instances != null)
        {
            HandleTime?.Invoke();
        }
    }

    public void ResetMap()
    {
        Destroy(parentCoreGame.GetChild(0).gameObject);
        bg.SetActive(false);
        menu.SetActive(false);
        foreach (Transform child in bg.transform.parent.transform)
        {
            child.gameObject.SetActive(false);
        }

        if (selectMapGameDemo.lastNameMap == "Map1") Instantiate(map1, parentCoreGame);
        if (selectMapGameDemo.lastNameMap == "Map2") Instantiate(map2, parentCoreGame);
        if (selectMapGameDemo.lastNameMap == "Map3") Instantiate(map3, parentCoreGame);
    }

    IEnumerator ShowMenu(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        menu.SetActive(true);
    }
}