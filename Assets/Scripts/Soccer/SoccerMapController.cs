using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerMapController : MonoBehaviour
{
    [SerializeField] private GameObject player2, player3, player4;

    void Awake()
    {
        Invoke("HandleComponentPlayer", 0.1f);
    }

    private void HandleComponentPlayer()
    {
        foreach (Transform button in UIInGameController.Instances.transform)
        {
            if (button.gameObject.activeInHierarchy && button.transform.name == "FixedJoystick")
            {
                Destroy(player2.GetComponent<SoccerPlayerController>());
                player2.GetComponent<AISoccerController>().enabled = true;
                Destroy(player3.GetComponent<SoccerPlayerController>());
                player3.GetComponent<AISoccerController>().enabled = true;
                Destroy(player4.GetComponent<SoccerPlayerController>());
                player4.GetComponent<AISoccerController>().enabled = true;
            }
            else if (button.gameObject.activeInHierarchy && button.transform.name == "FixedJoystick1")
            {
                player2.AddComponent<SoccerPlayerController>();
                Destroy(player2.GetComponent<AISoccerController>());
            }
            else if (button.gameObject.activeInHierarchy && button.transform.name == "FixedJoystick2")
            {
                player3.AddComponent<SoccerPlayerController>();
                Destroy(player3.GetComponent<AISoccerController>());
            }

            else if (button.gameObject.activeInHierarchy && button.transform.name == "FixedJoystick3")
            {
                player4.AddComponent<SoccerPlayerController>();
                Destroy(player4.GetComponent<AISoccerController>());
            }
        }
    }
    
}