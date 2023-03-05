using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeaBattleMap2Controller : MonoBehaviour
{
    public static SeaBattleMap2Controller Instances;
    [SerializeField] private GameObject player2, player3, player4;
    [SerializeField] CanonBattleSea[] cannonBattleSeas;
    int remainPlayer = 4;
    int playerWin = -1;
    public bool[] playersDead = new bool[4];
    void Awake()
    {
        Instances = this;
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

    public void SetInfoDeathPlayer(int id)
    {
        if (id == 0) playersDead[0] = true;
        else if (id == 1) playersDead[1] = true;
        else if (id == 2) playersDead[2] = true;
        else if (id == 3) playersDead[3] = true;
        CheckAndShowResult();
    }

    private void CheckAndShowResult()
    {
        int countFalse = 0;
        int lastFalseIndex = -1;

        for (int i = 0; i < playersDead.Length; i++) {
            if (!playersDead[i]) {
                countFalse++;
                lastFalseIndex = i;
            }
        }

        if (countFalse == 1) {
            CoreGameController.Instances.HandleShowResult("Player"+(lastFalseIndex + 1),null);
        }

    }
}
