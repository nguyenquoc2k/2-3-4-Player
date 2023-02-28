using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGroupRace : MonoBehaviour
{
    public static ToggleGroupRace Instances;
    public Toggle classic, knockout,deathMatch;
    private void Awake()
    {
        Instances = this;
        classic.isOn = true;
    }

    private void Update()
    {
        if (classic.isOn == true)
        {
            
        }
    }
}
