using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaBattleController : MonoBehaviour
{
    [SerializeField] private GameObject player2, player3, player4;

    void Awake()
    {
        Invoke("HandleJoystickPlayer", 0.1f);
        Invoke("HandleComponentPlayer", 0.2f);
    }

    void HandleJoystickPlayer()
    {
        if (SelectAmountPlayer.Instances.option1 == true)
        {
            SelectAmountPlayer.Instances.Joystick1.SetActive(true);
            SelectAmountPlayer.Instances.Joystick2.SetActive(false);
            SelectAmountPlayer.Instances.Joystick3.SetActive(false);
            SelectAmountPlayer.Instances.Joystick4.SetActive(false);
        }
        else if (SelectAmountPlayer.Instances.option2 == true)
        {
            SelectAmountPlayer.Instances.Joystick1.SetActive(true);
            SelectAmountPlayer.Instances.Joystick2.SetActive(true);
            SelectAmountPlayer.Instances.Joystick3.SetActive(false);
            SelectAmountPlayer.Instances.Joystick4.SetActive(false);
        }
        else if (SelectAmountPlayer.Instances.option3 == true)
        {
            SelectAmountPlayer.Instances.Joystick1.SetActive(true);
            SelectAmountPlayer.Instances.Joystick2.SetActive(true);
            SelectAmountPlayer.Instances.Joystick3.SetActive(true);
            SelectAmountPlayer.Instances.Joystick4.SetActive(false);
        }
        else if (SelectAmountPlayer.Instances.option4 == true)
        {
            SelectAmountPlayer.Instances.Joystick1.SetActive(true);
            SelectAmountPlayer.Instances.Joystick2.SetActive(true);
            SelectAmountPlayer.Instances.Joystick3.SetActive(true);
            SelectAmountPlayer.Instances.Joystick4.SetActive(true);
        }
    }
    private void HandleComponentPlayer()
    {
        foreach (Transform button in UIInGameController.Instances.transform)
        {
            if (button.gameObject.activeInHierarchy && button.transform.name == "FixedJoystick")
            {
                Destroy(player2.GetComponent<BowSeaBattleController>());
                Destroy(player3.GetComponent<BowSeaBattleController>());
                Destroy(player4.GetComponent<BowSeaBattleController>());
            }
            else if (button.gameObject.activeInHierarchy && button.transform.name == "FixedJoystick1")
            {
                player2.AddComponent<BowSeaBattleController>();
                Destroy(player2.GetComponent<AIBattleShip>());
            }
            else if (button.gameObject.activeInHierarchy && button.transform.name == "FixedJoystick2")
            {
                player3.AddComponent<BowSeaBattleController>();
                Destroy(player3.GetComponent<AIBattleShip>());
            }

            else if (button.gameObject.activeInHierarchy && button.transform.name == "FixedJoystick3")
            {
                player4.AddComponent<BowSeaBattleController>();
                Destroy(player4.GetComponent<AIBattleShip>());
            }
        }
    }
}