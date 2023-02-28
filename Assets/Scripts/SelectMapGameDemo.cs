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
    [SerializeField] private GameObject uiInGame, laps,scoreAndTime;
    public string lastNameMap;
    public bool soccerGame;

    private void Awake()
    {
        Instances = this;
    }

    public void OnClickMap1()
    {
        JoinGame(map1);
    }

    public void OnClickMap2()
    {
        JoinGame(map2);
    }

    public void OnClickMap3()
    {
        JoinGame(map3);
    }

    public void JoinGame(GameObject map)
    {
        if (ToggleGroupController.Instances!=null &&ToggleGroupController.Instances.gameMode3.isOn == true|| soccerGame==true)
        {
            scoreAndTime?.SetActive(true);
        }
        lastNameMap = map.name;
        uiInGame.SetActive(true);
        if(laps!=null) laps.SetActive(true);
        Instantiate(map, parentCoreGame);
        ListGameManager.Instances.listMaps.SetActive(false);
    }
}