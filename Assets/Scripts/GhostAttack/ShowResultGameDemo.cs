using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowResultGameDemo : MonoBehaviour
{
    public static ShowResultGameDemo Instances;
    public GameObject winner1, winner2, winner3, winner4, menu;
    public Transform parentCoreGame;
    [SerializeField] private GameObject map1,map2,map3;
    public DataSave dataSave;
    public SelectMapGameDemo selectMapGameDemo;
    private void Awake()
    {
        Instances = this;
        menu.SetActive(false);
    }

    public void ShowResult(string nameWinner)
    {
        menu.SetActive(false);
        if (nameWinner == "Player1")
        {
            winner1.SetActive(true);
            menu.SetActive(true);
        }

        if (nameWinner == "Player2")
        {
            winner2.SetActive(true);
            menu.SetActive(true);
        }

        if (nameWinner == "Player3")
        {
            winner3.SetActive(true);
            menu.SetActive(true);
        }

        if (nameWinner == "Player4")
        {
            winner4.SetActive(true);
            menu.SetActive(true);
        }

        SaveData();
    }

    private void SaveData()
    {
        dataSave.bool1 = dataSave.bool2 = dataSave.bool3 = dataSave.bool4 = false;
        GameObject Joystick1 = GameObject.Find("Joystick");
        GameObject Joystick2 = GameObject.Find("Joystick1");
        GameObject Joystick3 = GameObject.Find("Joystick2");
        GameObject Joystick4 = GameObject.Find("Joystick3");

        if (Joystick1 != null && Joystick1.GetComponent<Joystick>().gameObject.activeInHierarchy)
        {
            dataSave.bool1 = true;
        }
        if (Joystick2 != null && Joystick2.GetComponent<Joystick>().gameObject.activeInHierarchy)
        {
            dataSave.bool2 = true;
        }
        if (Joystick3 != null && Joystick3.GetComponent<Joystick>().gameObject.activeInHierarchy)
        {
            dataSave.bool3 = true;
        }
        if (Joystick4 != null && Joystick4.GetComponent<Joystick>().gameObject.activeInHierarchy)
        {
            dataSave.bool4 = true;
        }
    }

    public void OnClickMenu()
    {
        GameManager.Instances.lobbyManager.gameObject.SetActive(true);
        GameManager.Instances.BackToLobby();
    }

    public void OnClickPlayAgain()
    {
        Destroy(parentCoreGame.GetChild(0).gameObject);
        foreach (Transform child in winner1.transform.parent.transform)
        {
            child.gameObject.SetActive(false);
        }
        if(selectMapGameDemo.lastNameMap == "CoreGame" ) Instantiate(map2, parentCoreGame);
        if(selectMapGameDemo.lastNameMap == "CoreGame1" ) Instantiate(map1, parentCoreGame);
        if(selectMapGameDemo.lastNameMap == "CoreGame3" ) Instantiate(map3, parentCoreGame);
    }
    // IEnumerator ShowMenu(float seconds)
    // {
    //     yield return new WaitForSeconds(seconds);
    //     menu.SetActive(true);
    // }
}