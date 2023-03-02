using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class SelectAmountPlayer : MonoBehaviour
{
    public static SelectAmountPlayer Instances;
    [SerializeField] private GameObject uiInGame;
    public GameObject Joystick1, Joystick2, Joystick3, Joystick4;
    public bool option1, option2, option3, option4;
    private void Awake()
    {
        Instances = this;
        ListGameManager.Instances.AssignValue();
        ListGameManager.Instances.listMaps.SetActive(false);
    }

    public void OnClick1Player()
    {
        option1 = true;
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
        option2 = true;
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
        option3 = true;
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
        option4 = true;
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