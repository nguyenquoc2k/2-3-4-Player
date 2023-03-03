using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaBattleController : MonoBehaviour
{
    [SerializeField] private GameObject player2, player3, player4;

    void Awake()
    {
        Invoke("HandleComponentPlayer", 0.2f);
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