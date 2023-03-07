using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instances;
    public GameObject lobbyManager;
    public GameObject listGameManager;
    private void Awake()
    {
        Instances = this;
        
    }
    
    public void BackToLobby()
    {
        Destroy(ListGameManager.Instances.gameIsPlaying.transform.GetChild(0).gameObject);
        listGameManager.gameObject.SetActive(false);
    }
}