using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListGameManager : MonoBehaviour
{
    public static ListGameManager Instances;
    public GameObject amountPlayer;
    public GameObject listMaps;
    public GameObject uiSelectGame;
    public GameObject gameIsPlaying;

    private void Awake()
    {
        Instances = this;
        
    }

    public void HandleAmountAndListMaps(bool isActive)
    {
        amountPlayer.SetActive(isActive);
        listMaps.SetActive(!isActive);
    }
    private void OnEnable()
    {
        uiSelectGame.SetActive(true);
    }

    public void AssignValue()
    {
        amountPlayer = gameIsPlaying.transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
        listMaps = gameIsPlaying.transform.GetChild(0).GetChild(0).GetChild(2).gameObject;
        
     
    }
}