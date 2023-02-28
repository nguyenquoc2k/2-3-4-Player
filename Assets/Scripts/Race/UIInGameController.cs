using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInGameController : MonoBehaviour
{
    public static UIInGameController Instances;

    private void Awake()
    {
        Instances = this;
    }
}
