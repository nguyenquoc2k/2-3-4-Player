using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private GameObject listGame;

    private void Awake()
    {
        listGame.SetActive(false);
        GameManager.Instances.lobbyManager.SetActive(true);
    }

    public void OnClickPlayGame()
    {
        listGame.SetActive(true);
        GameManager.Instances.lobbyManager.SetActive(false);
    }
}
