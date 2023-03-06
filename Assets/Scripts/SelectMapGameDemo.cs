using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMapGameDemo : MonoBehaviour
{
    public static SelectMapGameDemo Instances;
    [SerializeField] private GameObject map1, map2, map3;
    [SerializeField] private Transform parentCoreGame;
    [SerializeField] private GameObject uiInGame, laps, scoreAndTime;
    public string lastNameMap;
    public bool modeTimeAndScore;

    private void Awake()
    {
        Instances = this;
    }

    public void OnClickMap1()
    {
        if (transform.GetComponent<SetGameModeSeaBattle>() != null) modeTimeAndScore = true;
        else if (transform.GetComponent<MapModeStickBall>() != null)
            transform.GetComponent<MapModeStickBall>().isCountdown = true;


        JoinGame(map1);
    }

    public void OnClickMap2()
    {
        if (transform.GetComponent<SetGameModeSeaBattle>() != null) modeTimeAndScore = false;
        else if (transform.GetComponent<MapModeStickBall>() != null)
            transform.GetComponent<MapModeStickBall>().isCountdown = false;

        JoinGame(map2);
    }

    public void OnClickMap3()
    {
        JoinGame(map3);
    }

    public void JoinGame(GameObject map)
    {
        if (ToggleGroupController.Instances != null && ToggleGroupController.Instances.gameMode3.isOn == true ||
            modeTimeAndScore == true)
        {
            scoreAndTime?.SetActive(true);
        }

        lastNameMap = map.name;
        uiInGame.SetActive(true);
        if (laps != null) laps.SetActive(true);
        Instantiate(map, parentCoreGame);
        ListGameManager.Instances.listMaps.SetActive(false);
    }
}