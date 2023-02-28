using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGameController : MonoBehaviour
{
    public static CoreGameController Instances;
    public static event Action<string, string> ShowResult;

    private void Awake()
    {
        Instances = this;
    }

    public void HandleShowResult(string transformName, string trasnformName2)
    {
        if (transformName != null)
        {
            if (trasnformName2 != null)
            {
                ShowResult?.Invoke(transformName, trasnformName2);
            }
            else
            {
                ShowResult?.Invoke(transformName, null);
            }
        }
        else if (MapStickmanRunController.Instances != null &&
                 MapStickmanRunController.Instances.listPlayers.Count == 1)
        {
            ShowResult?.Invoke(MapStickmanRunController.Instances.listPlayers[0].name, null);
        }
        else if (transformName == null && trasnformName2 == null)
        {
            ShowResult?.Invoke(null, null);
        }
    }
}