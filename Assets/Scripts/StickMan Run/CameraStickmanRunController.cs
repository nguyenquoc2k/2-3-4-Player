using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStickmanRunController : MonoBehaviour
{
    public static CameraStickmanRunController Instances;

    private void Awake()
    {
        Instances = this;
    }
}
