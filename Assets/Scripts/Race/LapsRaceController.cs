using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LapsRaceController : MonoBehaviour
{
    public static LapsRaceController Instances;
    public TextMeshProUGUI lapCar1, lapCar2, lapCar3,lapCar4;
    private void Awake()
    {
        Instances = this;
        CarRaceController.LapsCounting += ShowRound;
    }

    private void OnDestroy()
    {
       CarRaceController.LapsCounting -= ShowRound;
    }

    private void ShowRound(string nameCar, int lapNumber)
    {
        if (nameCar == "Player1")
        {
            lapCar1.text = lapNumber + "/3";
        }
        if (nameCar == "Player2")
        {
            lapCar2.text = lapNumber + "/3";
        }
        if (nameCar == "Player3")
        {
            lapCar3.text = lapNumber + "/3";
        }
        if (nameCar == "Player4")
        {
            lapCar4.text = lapNumber + "/3";
        }
    }
}
