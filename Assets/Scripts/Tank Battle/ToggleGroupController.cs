using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGroupController : MonoBehaviour
{
    public static ToggleGroupController Instances;
    public Toggle gameMode1, gameMode2,gameMode3;
    public GameObject mapMode1, mapMode2, mapMode3;
    private void Awake()
    {
        Instances = this;
        gameMode1.isOn = true;
    }

    private void Update()
    {
        if (gameMode1.isOn == true)
        {
            mapMode1.SetActive(true);
            mapMode2.SetActive(false);
            mapMode3.SetActive(false);
            
        }
        else if (gameMode2.isOn == true)
        {
            mapMode2.SetActive(true);
            mapMode1.SetActive(false);
            mapMode3.SetActive(false);
        }
        else if (gameMode3.isOn == true)
        {
            mapMode3.SetActive(true);
            mapMode1.SetActive(false);
            mapMode2.SetActive(false);
        }
    }
}
