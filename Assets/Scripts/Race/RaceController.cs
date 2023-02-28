using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    public bool classicMode,knockoutMode;
    public List<GameObject> listCar = new List<GameObject>();
    public Transform groupParentCar;
    [SerializeField] private GameObject player2, player3, player4;
    public static bool isDone3Lap = false;
    private void Awake()
    {
        
        if (ToggleGroupRace.Instances.classic.isOn == true)
        {
            classicMode = true;
            knockoutMode = false;
        }
        else if (ToggleGroupRace.Instances.knockout.isOn == true)
        {
            classicMode = false;
            knockoutMode = true;
        }
        Invoke("HandleComponentPlayer", 0.1f);
    }
    private void HandleComponentPlayer()
    {
        foreach (Transform button in UIInGameController.Instances.transform)
        {
            if (button.gameObject.activeInHierarchy && button.transform.name == "FixedJoystick")
            {
                player2.GetComponent<CarRaceController>().enabled = false;
                player2.GetComponent<AICarRacePaths>().enabled = true;
                player3.GetComponent<CarRaceController>().enabled = false;
                player3.GetComponent<AICarRacePaths>().enabled = true;
                player4.GetComponent<CarRaceController>().enabled = false;
                player4.GetComponent<AICarRacePaths>().enabled = true;
            }
            else if (button.gameObject.activeInHierarchy && button.transform.name == "FixedJoystick1")
            {
                player2.GetComponent<CarRaceController>().enabled = true;
                player2.GetComponent<AICarRacePaths>().enabled = false;
            }
            else if (button.gameObject.activeInHierarchy && button.transform.name == "FixedJoystick2")
            {
                player3.GetComponent<CarRaceController>().enabled = true;
                player3.GetComponent<AICarRacePaths>().enabled = false;
            }

            else if (button.gameObject.activeInHierarchy && button.transform.name == "FixedJoystick3")
            {
                player4.GetComponent<CarRaceController>().enabled = true;
                player4.GetComponent<AICarRacePaths>().enabled = false;
            }
        }
    }
    public void HandleListCar(string nameCar)
    {
        for (int i = listCar.Count - 1; i >= 0; i--)
        {
            if (listCar[i].transform.name == nameCar)
            {
                listCar.RemoveAt(i);
            }
        }
        if (listCar.Count == 1)
        {
            Destroy(listCar[0].gameObject);
            listCar.RemoveAt(0);
            Invoke("AddPlayerToList",0.1f);
            
        }
    }

    void AddPlayerToList()
    {
        foreach (Transform child in groupParentCar)
        {
            listCar.Add(child.gameObject);
        }
    }
}