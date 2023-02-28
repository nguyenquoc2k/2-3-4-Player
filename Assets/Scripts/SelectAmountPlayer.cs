using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class SelectAmountPlayer : MonoBehaviour
{
    [SerializeField] private GameObject uiInGame;
    [SerializeField] GameObject Joystick1, Joystick2, Joystick3, Joystick4;
    private void Awake()
    {
        ListGameManager.Instances.AssignValue();
        ListGameManager.Instances.listMaps.SetActive(false);
    }

    public void OnClick1Player()
    {
        uiInGame.SetActive(true);
        Joystick1.SetActive(true);
        Joystick2.SetActive(false);
        Joystick3.SetActive(false);
        Joystick4.SetActive(false);
        uiInGame.SetActive(false);
        SelectMap();
    }

    public void OnClick2Player()
    {
        uiInGame.SetActive(true);
        Joystick1.SetActive(true);
        Joystick2.SetActive(true);
        Joystick3.SetActive(false);
        Joystick4.SetActive(false);
        uiInGame.SetActive(false);
        SelectMap();
    }

    public void OnClick3Player()
    {
        uiInGame.SetActive(true);
        Joystick1.SetActive(true);
        Joystick2.SetActive(true);
        Joystick3.SetActive(true);
        Joystick4.SetActive(false);
        uiInGame.SetActive(false);
        SelectMap();
    }

    public void OnClick4Player()
    {
        uiInGame.SetActive(true);
        Joystick1.SetActive(true);
        Joystick2.SetActive(true);
        Joystick3.SetActive(true);
        Joystick4.SetActive(true);
        uiInGame.SetActive(false);
       
        SelectMap();
    }
    
    void SelectMap()
    {
        ListGameManager.Instances.HandleAmountAndListMaps(false);
    }
}