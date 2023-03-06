using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModeStickBall : MonoBehaviour
{
    public static MapModeStickBall Instances;
    public bool isCountdown;

    private void Awake()
    {
        Instances = this;
    }
}