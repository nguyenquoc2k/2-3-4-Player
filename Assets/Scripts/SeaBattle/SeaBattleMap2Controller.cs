using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeaBattleMap2Controller : MonoBehaviour
{
    [SerializeField] private GameObject player2, player3, player4;
    [SerializeField] CanonBattleSea[] cannonBattleSeas;
    int remainPlayer = 4;
    int playerWin = -1;
    [SerializeField] bool[] playersDead = new bool[4];
    void Awake()
    {
        Invoke("HandleComponentPlayer", 0.1f);
        foreach (var cannon in cannonBattleSeas)
        {
            cannon.SuffleBoatList();
        }
    }
    
    private void HandleComponentPlayer()
    {
        foreach (Transform button in UIInGameController.Instances.transform)
        {
            if (button.gameObject.activeInHierarchy && button.transform.name == "FixedJoystick")
            {
                Destroy(player2.transform.GetChild(0).GetComponent<BoatMoveBattleShip>());
                Destroy(player3.transform.GetChild(0).GetComponent<BoatMoveBattleShip>());
                Destroy(player4.transform.GetChild(0).GetComponent<BoatMoveBattleShip>());
            }
            else if (button.gameObject.activeInHierarchy && button.transform.name == "FixedJoystick1")
            {
                player2.transform.GetChild(0).AddComponent<BoatMoveBattleShip>();
                Destroy(player2.transform.GetChild(0).GetComponent<AIBattleShip>());
            }
            else if (button.gameObject.activeInHierarchy && button.transform.name == "FixedJoystick2")
            {
                player3.transform.GetChild(0).AddComponent<BoatMoveBattleShip>();
                Destroy(player3.transform.GetChild(0).GetComponent<AIBattleShip>());
            }

            else if (button.gameObject.activeInHierarchy && button.transform.name == "FixedJoystick3")
            {
                player4.transform.GetChild(0).AddComponent<BoatMoveBattleShip>();
                Destroy(player4.transform.GetChild(0).GetComponent<AIBattleShip>());
            }
        }
    }
}
